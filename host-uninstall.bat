:: Deletes the entry created by install_host.bat
@echo off
REG DELETE "HKCU\Software\Google\Chrome\NativeMessagingHosts\com.zcc.macaddr.nativemessage" /f
echo "uninstall complete!!!"
pause