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
        "AzureAd:ClientId": "<AzureAD ClientId>",
        "AzureAd:ClientSecret": "<AzureAD ClientSecret>",
        "AuthMessageSenderOptions_Domain": "<Domain für SMTP Server>",
        "AuthMessageSenderOptions_UserName": "<Nutzername für SMTP Server>",
        "AuthMessageSenderOptions_SentFrom": "<Anzuzeigende Email-Adresse für SMTP Server>",
        "AuthMessageSenderOptions_Password": "<Passwort für SMTP Server>",
        "AuthMessageSenderOptions_SMTPClient": "<SMTP Client Adresse>"
    }

### Datenbank:
Um die App starten zu können wird eine laufende MySQL Datenbank vorausgesetzt: [Download](https://dev.mysql.com/downloads/mysql/). Der default connection String ist auf `server=localhost;port=3306;database=personaltool;user=personaltool;password=personaltool;` festgelegt, was bedeutet dass MySQL lokal auf dem Rechner läuft, Port 3306 benutzt, die Datenbank `personaltool` verwendet wird und der Benutzer `personaltool` mit dem Passwort `personaltool` benutzt wird. Alternativ kann dieser connection String auch in den Secrets unter dem Schlüssel `ConnectionStrings:DefaultConnection` überschrieben werden, falls die lokale installation andere Werte erfordert. Wichtig ist, dass der angegebene Benutzer Schreibrechte auf die angegebene Datenbank hat.

Die von der App verwendete Datenbank wird mittels des EntityFramework Code-First-Migration Konzepts entwickelt. Die Migrationen zur Erzeugung der Datenbank sind im Code enthalten. Um die Datenbank zu erzeugen oder zu aktualisieren, muss der EntityFramework Befehl über die Konsole ausgeführt werden:  
In Visual Studio Packet-Manager-Konsole:

    PM> Update-Database

In Linux/Mac Konsole:

    > dotnet ef database update

Weitere Informationen bezüglich Migrationen findet sich in der offiziellen [Dokumentation](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/).

#### Datenbank Testing

    "RandomTestSeedConfig": {
        "Enabled": false, // this entire feature is disabled by default
        "ClearExistingData": false, // if all existing data should be removed, doesn't remove Persons connected to Accounts
        "Seed": null, // setting this to null causes the seed to be random, if it's a number, it's choosen as the seed
        "Persons": 10,
        "Positions": 5,
        "CareerLevels": 4,
        "MemberStatus": 3,
        "PersonPositions": 10,
        "PersonCareerLevels": 6,
        "PersonMemberStatus": 3
    }
