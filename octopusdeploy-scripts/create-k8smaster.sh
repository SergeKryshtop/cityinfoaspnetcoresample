K8S_MASTER=$1 #pass IP of Kubernetes master as parameter or replace with string i.e. 10.0.0.6

#create k8es master node

sudo su
#*********************************'
# INSTALLING DOCKER
#**********************************

#Make sure your existing packages are up-to-date.
sudo yum -y update
#disable firewall
sudo systemctl stop firewalld
sudo systemctl disable firewalld
sudo setenforce permissive

#Add the yum repo
sudo tee /etc/yum.repos.d/docker.repo <<-'EOF'
[dockerrepo]
name=Docker Repository
baseurl=https://yum.dockerproject.org/repo/main/centos/7/
enabled=1
gpgcheck=1
gpgkey=https://yum.dockerproject.org/gpg
EOF

#Install the Docker package.
sudo yum -y install docker-engine

#Enable the service and run it on start
sudo systemctl enable docker.service
sudo systemctl enable docker


#Start the Docker daemon.
 
sudo systemctl start docker
 
#Verify docker is installed correctly by running a test image in a container.
sudo docker run --rm hello-world

#*************************************
# Patching CoreOS for Docker
#*************************************
#The following command fixes the bug with wrong position of names in Docker engine:
if [ "$(id -u)" -ne "0" ]; then
 echo -e "\n"
 echo -e "This script must be run as root! \n" 1>&2
 exit 1
fi

RESULT=$?

touch /etc/rc.d/init.d/cgroups && chmod +x /etc/rc.d/init.d/cgroups

if [ "$RESULT" -ne "0" ]; then
        >$2 echo -e "\n"
        >&2 echo -e "cgroups file wasn't created ! \n"
        exit 1
else
        echo -e "\n"
        echo -e "cgroups file created succesfully! \n"
        echo "mount -o remount,rw '/sys/fs/cgroup'" > /etc/rc.d/init.d/cgroups
        echo "ln -s /sys/fs/cgroup/cpu,cpuacct /sys/fs/cgroup/cpuacct,cpu" >>  /etc/rc.d/init.d/cgroups
fi


echo "/etc/rc.d/init.d/cgroups" >> /etc/rc.d/rc.local && chmod +x /etc/rc.d/rc.local

if [ "$RESULT" -ne "0" ]; then
        >&2 echo -e "Can't change the rc.local file! \n"
        exit 1
else
        echo -e "rc.local file changed succesfully! \n"
fi

echo -e "cgroups bug fixed succesfully! \n"
exit 0

#The following commands fixes the bug with Docker bootstrap
sudo su

sudo cat > /lib/systemd/system/docker-bootstrap.socket <<EOF
[Unit]
Description=Docker Socket for the API
PartOf=docker-bootstrap.service

[Socket]
ListenStream=/var/run/docker-bootstrap.sock
SocketMode=0660
SocketUser=root
SocketGroup=docker

[Install]
WantedBy=sockets.target
EOF

sudo cat > /lib/systemd/system/docker-bootstrap.service <<EOF
[Unit]
Description=Docker Application Container Engine
Documentation=https://docs.docker.com
After=network.target docker-bootstrap.socket
Requires=docker-bootstrap.socket

[Service]
ExecStart=/usr/bin/docker daemon -H unix:///var/run/docker-bootstrap.sock -p /var/run/docker-bootstrap.pid --iptables=false --ip-masq=false --bridge=none --graph=/var/lib/docker-bootstrap --exec-root=/var/run/docker-bootstrap
RestartSec=10s
Restart=on-failure
MountFlags=shared
LimitNOFILE=1048576
LimitNPROC=1048576
LimitCORE=infinity

[Install]
WantedBy=multi-user.target
EOF
chmod 644 /lib/systemd/system/docker-bootstrap.socket
chmod 644 /lib/systemd/system/docker-bootstrap.service
sudo systemctl enable docker-bootstrap

#Configure Docker to start on boot
sudo systemctl enable docker

#*******************************************
# INSTALLING KUBERNETES MASTER
#*******************************************
sudo mount -o remount,rw '/sys/fs/cgroup'
sudo ln -s /sys/fs/cgroup/cpu,cpuacct /sys/fs/cgroup/cpuacct,cpu
 
sudo systemctl stop firewalld
sudo systemctl disable firewalld
export K8S_VERSION=v1.4.3
#clone the kube-deploy repo, and run master.sh
git clone https://github.com/kubernetes/kube-deploy 
cd kube-deploy/docker-multinode
./master.sh

#installing kubectl
curl -ssl https://storage.googleapis.com/kubernetes-release/release/v1.4.3/bin/linux/amd64/kubectl > /usr/bin/kubectl

chmod +x /usr/bin/kubectl

kubectl cluster-info
