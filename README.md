# Licznik fauli na Elizie
W warszawskiej drużynie Quidditcha Warsaw Mermaids, na treningach dzieją się różne rzeczy. Eliza ma tyle pecha, że jest często faulowana. Napisałem więc aplikację w F#, która liczy ile razy ją sfaulowano.

### Azure
Aplikacja jest przygotowana do hostowania na Azurze (Web App). Wystarczy skompilować **na Windowsie** i przerzucić plik App.exe, biblioteki DLL i web.config do `$HOME\site\wwwroot`.

Z jakichś powodów xbuild/fsharpc ustawia niewłaściwie referencję do FSharp.Core, co uniemożliwia uruchomienie aplikacji na Azurze (choć lokalnie działa).
