# Reflektion

## REST

I projektet finns fyra entiteter som lagras i databasen: Film, FilmStudio, Rental och User. Eftersom en filmstudio också ska ha ett användarkonto lagras infon om filmstudion (id, namn, stad) i FilmStudio-tabellen medan själva kontoinformationen (t.ex. användarnamn, lösenord, GUID) lagras i Users (där också administratörer finns). Både en FilmStudio-entitet och en User-entitet skapas alltså när man registrerar en filmstudio. Dessa kopplas samman med den icke-obligatoriska egenskapen FilmStudioId på Users. På klassen User finns också FilmStudio (som ska vara ett FilmStudio-objekt), som finns för att studioinformation ska följa med när användarinfo exponeras i API:et. Denna lagras dock inte i databasen då objekt inte kan lagras i en SQL-databas, varför studio-ID:t fortfarande fyller en funktion.

Jag valde alltså att skapa en klass för uthyrningar. Ett alternativt sätt hade varit att skapa en klass för FilmCopy som innehåller vilken film exemplaret är av och vem som eventuellt hyr den. Fördelen med min metod, där man har med uthyrnings-ID, film-ID, studio-ID, samt en boolean för ifall filmen är tillbakalämnad eller inte, är att man då också datan för avslutade uthyrningar sparas. Hur många exemplar som finns tillgängliga hanteras genom egenskapen AvailableCopies på Film.

I flera fall hade de kravställda endpointarna en avvikande struktur på sin URL (t.ex. /api/mystudio/rentals). Dessa placerades då i den controller där de hörde bäst hemma utifrån vad de hanterade. URL:en overridades genom att specificera exempelvis ("/api/mystudio/rentals") efter HTTP-metoden. På så sätt kan man hantera endpoints med avvikande struktur utan att behöva skapa specifika controllers för de fallen.



## Implementation

För att kontrollera vad som exponeras via API:et används DTO:er. I vissa fall är detta inte nödvändigt då man vill exponera alla egenskaper i en klass, och då kan de interna modellerna användas istället för en DTO. I ett fall (vid inloggning) returneras ett anonymt objekt istället för en DTO eftersom den specifika datastrukturen bara var applicerbar i det fallet. Jag uppfattar det som ett bra sätt i sådana situationer eftersom man då slipper en DTO som ökar komplexiteten. Med hjälp av DTO:er eller anonyma objekt kan man inte bara begränsa vilken information man vill exponera, utan även lägga till ytterligare egenskaper, t.ex. hämta och skicka med ytterligare info som inte ingår i det ursprungliga objektet (t.ex. när man returnerar en uthyrning så kan man skicka med hela objektet för den hyrda filmen).

Många av DTO:erna följer de interfaces som fanns definierade i kravspecifikationen. För enhetlighetens skull hade det dock varit bättre att antingen inte använda interfaces för DTO:er eller att använda det för alla.

För att separarera ansvar har jag använt mig av repositories som hanterar alla databasoperationer. Det finns även en HelperServices-klass som innehåller små metoder som är återanvändbara men som inte direkt hanterar några databasoperationer. Denna injiceras, precis som repositoryna, med hjälp av DI för att kunna användas från vilken metod som helst.


## Säkerhet

För användarhantering har jag byggt egna registrerings- och autentiseringsendpoints och inte använt mig av Core Identity eller något annat bibliotek. Detta möjliggjorde större flexibilitet i vilken användardata som kan sparas. 

Vid registrering av en användare genereras ett GUID som returneras i svaret när användaren gör en lyckad inloggning. Detta GUID sparas i localStorage och används sedan i Authorization-headern när anrop görs. På detta sätt kan backenden både kontrollera att användaren finns, men också hämta info om användaren och på så sätt veta vem den är, vad den har för roll osv. Säkerhetsmässigt finns det brister i denna metod, bland annat att GUID:et kommer gälla för alltid, och skulle det hamna i orätta händer kan en obehörig få oändlig tillgång till kontot, både genom att göra anrop med GUID:t men också genom att logga in sig i webbgränssnittet genom att sätta GUID:et i localStorage. En säkrare lösning hade kunnat vara att istället returnera en tidsbegränsad sessionstoken.

När det gäller in- och utloggning i webbgränssnittet lagras som sagt dels användarens GUID, men även studions ID-nummer (förutsatt att den inloggade användaren är en filmstudio). Syftet med detta är att vid uthyrning och återlämning kunna skicka med studions ID utan att varje gång behöva hämta det på nytt. Eftersom studions ID också kontrolleras mot GUID:et vid anrop kan man inte hyra åt någon annan genom att ändra värdet i localStorage. ID-numret i localStorage kan också användas för att utan nya anrop kontrollera om användaren är en filmstudio eller administratör, eftersom nyckeln bara finns om man är inloggad som filmstudio.

Vid utloggning rensas alla värden i localStorage och sidan laddas om för att rendera de utloggade vyerna.