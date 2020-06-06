# Personaltool

Das Personaltool ist eine WebApp zur Mitgliederverwaltung verwendet und entwickelt vom Campus Consult e. V.

Das Projekt basiert auf dem [ASP.NET Core 3.1 MVC](https://docs.microsoft.com/de-de/aspnet/core/mvc/overview?view=aspnetcore-3.1) Framework von Microsoft.

## Projekt einrichten:

1. Repository klonen oder herunterladen.
2. Benötigte Benutzergeheimnisse anlegen. ([Siehe hier](###Benutzergeheimnisse))
3. Datenbank migrieren. ([Siehe hier](###Datenbank))
4. App starten.

### Benutzergeheimnisse:
Diese App verwendet einige Benutzergeheimnisse, die vor dem Start der App konfiguriert werden müssen. Diese können mit dem [hier](https://docs.microsoft.com/de-de/aspnet/core/security/app-secrets?view=aspnetcore-3.1) erklärten Secret-Manager-Tool eingerichtet werden.

Die benötigten Schlüssel sind dem folgenden Auszug aus der secrets.json zu entnehmen und müssen mit den zu verwendenden Geheimnissen gefüllt werden:

    {
        "Authentication:AzureAd:ClientSecret": "<AzureAD ClientSecret",
        "AuthMessageSenderOptions_Domain": "<Domain für SMTP Server>",
        "AuthMessageSenderOptions_UserName": "<Nutzername für SMTP Server>",
        "AuthMessageSenderOptions_SentFrom": "<Anzuzeigende Email-Adresse für SMTP Server>",
        "AuthMessageSenderOptions_Password": "<Passwort für SMTP Server>",
        "AuthMessageSenderOptions_SMTPClient": "<SMTP Client Adresse>"
    }

### Datenbank:
Die von der App verwendete Datenbank wird mittels des EntityFramework Code-First-Migration Konzepts entwickelt. Die Migrationen zur Erzeugung der Datenbank sind im Code enthalten. Um die Datenbank zu erzeugen oder zu aktualisieren, muss der EntityFramework Befehl über die Konsole ausgeführt werden:
In Visual Studio Packet-Manager-Konsole:

    PM> Update-Database

In Linux/Mac Konsole:

    > dotnet ef database update


