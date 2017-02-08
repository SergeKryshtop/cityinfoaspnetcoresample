yum install bison gettext glib2 freetype fontconfig libpng libpng-devel libX11 libX11-devel glib2-devel libgdi* libexif glibc-devel urw-fonts java unzip gcc gcc-c++ automake autoconf libtool make bzip2 wget

cd /usr/local/src 

wget http://download.mono-project.com/sources/mono/mono-3.0.1.tar.bz2

tar jxf mono-3.0.1.tar.bz2

cd mono-3.0.1

./configure --prefix=/opt/mono

make && make install



yum install yum-utils
rpm --import "http://keyserver.ubuntu.com/pks/lookup?op=get&search=0x3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF"
yum-config-manager --add-repo http://download.mono-project.com/repo/centos/
yum install mono-complete
