:: %~dp0 is the directory containing this bat script and ends with a backslash.
@echo off
REG ADD "HKCU\Software\Google\Chrome\NativeMessagingHosts\com.zcc.macaddr.nativemessage" /ve /t REG_SZ /d "%~dp0com.zcc.macaddr.nativemessage.json" /f
echo "install complete!!!"
pause