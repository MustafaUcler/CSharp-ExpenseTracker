# CSharp-ExpenseTracker

Funktionalitet
Skriv ett interaktivt konsolprogram som låter användaren hålla koll på sina utgifter. Programmet ska innehålla följande funktionalitet:
•	Lägga till utgifter
o	Varje utgift består av namn, pris (inklusive moms) och kategori (Utbildning, Böcker, Livsmedel, eller Övrigt)
•	Visa alla enskilda utgifter (enbart inklusive moms) och deras kategorier samt antalet utgifter och totalsumman (både inklusive och exklusive moms) av dessa utgifter
•	Visa totalsumman (både inklusive och exklusive moms) av alla utgifter per kategori
•	Ändra på en utgift
•	Ta bort en enskild utgift
•	Ta bort alla utgifter
•	Se slutet av detta dokument för exempel på all funktionalitet som beskrivs ovan.
Utgå från startkoden i ExpenseTracker.
För att låta användaren välja alternativ ska ert program använda sig av ShowMenu, som finns beskriven i Advanced console features.
Regler för moms
Ert program ska hantera moms (VAT eller value-added tax på engelska) på följande sätt:
•	När användaren matar in en ny utgift ska enbart totalpriset (inklusive moms) matas in.
•	Programmet ska automatiskt lista ut momssatsen för en utgift baserat på kategorin:
o	Utbildning: 0%
o	Böcker: 6%
o	Livsmedel: 12%
o	Övrigt: 25%
•	Visa samtliga värden med två decimaler.
o	Exempel: 12.34 kr
o	Gör denna avrundning med value.ToString("0.00").
o	Använd inte Math.Round, som ger fel resultat om värdet har färre än två siffror efter decimalen.

Övrigt
•	Skriv kortfattade kommentarer i er kod för att förklara de viktigaste och/eller svåraste delarna. Ni varken bör eller behöver kommentera varje enskild rad.
•	Om ni märker att ni behöver göra samma sak på flera ställen och det kräver mer än ett par rader kod, försök hitta sätt att förkorta detta genom att definiera metoder som ni kan anropa istället för att upprepa koden.
•	Rensa konsolen med Console.Clear varje gång användaren har valt ett alternativ i huvudmenyn.
•	decimal fungerar i princip som double men används generellt när man hanterar pengar, eftersom den inte råkar ut för samma avrundningsfel som double. Använd decimal på samtliga ställen i programmet där ni hanterar just ett pengavärde så bör resten fungera som väntat.
o	En skillnad gentemot double är att om man skriver ett decimal-värde direkt i källkoden så behöver det följas av tecknet m. Exempelvis: decimal x = 100m.
•	Ni kan utgå från att användaren matar in giltiga värden. Ni behöver alltså inte ha felhantering för exempelvis inläsning av decimaltal.


 
