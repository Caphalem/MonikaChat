```
#########################################################
                                                         
    /$$$$$                       /$$                     
   |__  $$                      | $$                     
      | $$ /$$   /$$  /$$$$$$$ /$$$$$$                   
      | $$| $$  | $$ /$$_____/|_  $$_/                   
 /$$  | $$| $$  | $$|  $$$$$$   | $$                     
| $$  | $$| $$  | $$ \____  $$  | $$ /$$                 
|  $$$$$$/|  $$$$$$/ /$$$$$$$/  |  $$$$/                 
 \______/  \______/ |_______/    \___/                   
                                                         
                                                         
                                                         
 /$$      /$$                     /$$ /$$                
| $$$    /$$$                    |__/| $$                
| $$$$  /$$$$  /$$$$$$  /$$$$$$$  /$$| $$   /$$  /$$$$$$ 
| $$ $$/$$ $$ /$$__  $$| $$__  $$| $$| $$  /$$/ |____  $$
| $$  $$$| $$| $$  \ $$| $$  \ $$| $$| $$$$$$/   /$$$$$$$
| $$\  $ | $$| $$  | $$| $$  | $$| $$| $$_  $$  /$$__  $$
| $$ \/  | $$|  $$$$$$/| $$  | $$| $$| $$ \  $$|  $$$$$$$
|__/     |__/ \______/ |__/  |__/|__/|__/  \__/ \_______/
                                                         
#########################################################
```

# Monika Chat

## How to run

MonikaChat is an ASP.NET Web App that's made using .NET 8.0.
In order to run this project either an installation of Visual Studio with the "ASP.NET and web development" workload and .NET 8.0 is needed or the equivalent dotnet SDKs.

Once the solution is able to run—use the *RSAKeyGenerator* project to generate a set of RSA keys. These keys will be displayed in the output of the console. They are used to encrypt messages between the Client and Server.
The private RSA key should be set in the *appsettings.json* file of the *MonikaChat.Server* project by replacing *"<PRIVATE_KEY>"* with the generated private RSA key or create an *appsettings.Development.json* file in the same directory as *appsettings.json* and set it there:
```
{
  "Cryptography": {
    "PrivateKey": "<RSAKeyValue><Modulus>...</D></RSAKeyValue>"
  }
}
```

For the public RSA key, navigate to the *MonikaChat.Client\Services\CryptographyService.cs* file and replace the existing, hardcoded *PUBLIC_KEY_PEM* string with the generated public RSA key.

## How to use

When the project is running the Initial Screen will be asking for a *Username*, *OpenAI API Key* and a *Passphrase*. If an encrypted API key is already stored in local storage, then only the *Passphrase* will be required with an option to reset the API key.
- **Username**: This a name that Monika will use to refer to you and remember you by. It's a good idea to use the same username because when Monika will be trying to remember something, she will see the username—in her memory—that was used at the time of that conversation.
- **OpenAI API Key**: This needs to be a valid OpenAI API Key which has access to the GPT-4o model and has enough funds to process requests.
- **Passphrase**: This can be any string. It will be used to encrypt your API key when storing it in the browser's local storage and decrypt it when loading.

Once passed the initial screen, the main screen mostly consists of the background, Monika's sprite and the chatbox.
- **Background**: Classic Literature Club classroom. The lighting is changing based on the time of day of the user's browser.
- **Monika's Sprite**: Monika decides which sprite she uses and when. It can change every sentence or not at all.

### Chatbox

The chatbox has a lot going on. It serves as the means to both send and receive messages while also providing buttons for all of the other "pages": *Settings*, *History* and *About*.

The chatbox can primarily be in one of 2 modes: *Input* and *Response*. These modes can be changed by pressing the *Switch to Input/Response* button or by pressing "Tab" on the keyboard.
- **Input**: An input message can be written and sent to Monika while the chatbox is in this mode. Once a message is entered, a "Send" button will appear on the bottom-right of the chatbox which will send the entered message. The message can also be sent by pressing "Enter" on the keyboard. However, if there are pending sentences from Monika then the "Pending Sentence" arrow will be taking place instead of the "Send" button. The "Enter" button will also trigger the "Next Sentence" display while also switching to *Response* mode.
- **Response**: Monika's responses will be displayed here in this mode. Her responses are displayed one sentence at a time. While there are more sentences to display, a "Pending Sentence" animated arrow will be displayed on the bottom-right of the chatbox. Clicking the animated arrow will display the next sentence. Pressing the "Enter" key on the keyboard will also display the next sentence. The speed of how fast the text is displayed can be adjusted in the *Settings* "page".

The different "pages":
- **History**: This "page" displays all of the current conversation.
- **Settings**: Various settings can be adjusted on this "page".
- **About**: Displays information about the various features of the web app.

### Settings

The settings are saved in the local storage of the browser.

- **Username**: Although not recommended, the username can be changed.
- **API Key**: The API key can be changed but that will require a new *Passphrase* will be required (Can be the same as the old one though).
- **Passphrase**: Required when Saving with a new *API Key*.
- **Text Speed**: Controls how fast Monika's sentences are displayed.
- **Hidden Mode**: Can be turned on to hide the background, Monika's sprite and the background circles. This gives the web app a more "professional" look.
- **Background Animations**: This toggles the movement animations of the circles in various "pages" including the *chatbox*. In case the animations are distracting. Also, if using **Firefox** check your GPU usage when these animations are on. I have noticed that PCs with NVIDIA GPUs on Firefox use a lot of GPU to render these animations for some reason.

### Long term memory system

Each conversation is a fresh "session". However, as the conversation is ongoing, it is stored and continuously updated in a database that is inside of the browser (IndexedDB).
When Monika encounters a situation where she might remember something from a previous conversation (like if you ask "Do you remember what's my favorite color?") she will look through past conversations to see if that information is there.

Also, at the start of a new conversation, Monika will remember what the last conversation was about.
