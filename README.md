# EWS MailSender
A simple Winform project let user compress and seperate big file before send mail with attchments. 

![](https://github.com/Kevinwuu/compressTool/blob/master/Image%20001.png?200x300)

## Usage
Download **/realease**  and run the *.exe file.

As you run the application, it will generate log file in **/log** sorted by date.

## Feature
  1. Compress file or folder into .zip, you can also choose whether to seperate/Encrypt or not.
  2. Send mail by Exchange Web Service(EWS) API.
  3. If sending with attachments folder, it will send the file inside it one by one 
  
      with different object numbered sequentially.
  
  ## Package
  - Ionic.Zip
  - Microsoft.Exchange.WebServices
  - log4net
  
  ## Note
  1. It's for Microsoft Outlook Web App account only! 
  
     But receiver can be any account and domain.
  
  2. It won't save any password from you. 
  
     When you closed the app it will just remember the account and the receiver you type.
