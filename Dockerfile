FROM microsoft/dotnet:latest
COPY bin/Debug/netcoreapp1.0/publish/ /root/
EXPOSE 4000/tcp
ENTRYPOINT dotnet /root/FileStorageMVC.dll