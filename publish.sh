# script to build the project and upload it via SSH to a remote server
dotnet publish -c Release
cd "bin/Release/netcoreapp3.1"; tar cvzf - "publish" | ssh $SSH_LOGIN "cd /tmp; rm -r publish; tar zxvf -"