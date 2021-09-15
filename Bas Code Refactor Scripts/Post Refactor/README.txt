Ik heb bij het refactoren een erg grote algemene player controller opgebroken in kleinere specifiekere stukken:

-Alle movement code is verplaatst naar het PlayerMovement script. 
 dit lijkt me logischer omdat ik bijvoorbeeld een keer de movement uit wil zetten, maar niet de player logica.

-ik heb alle code die met UI te maken heeft uit PlayerController en GameManager gehaald, en verplaatst naar UImanager.
 er is nu een EventManager die regelt dat er events worden gestuurd om de UI te updaten via de UImanager, inplaats van rechtstreeks en via GameManager

-De DamageIndicator heeft nu een eigen script, omdat ik het gewoon niet erg logisch vond om het in Player te laten en ik er nu toch een Event voor heb.

-OnCollisionEnter heb ik vervangen met een (wel werkende) versie van IDamageable die nu via HealthComponent werkt. ik had in playerscript
 met damage zitten rommelen, maar nog niet opgeruimt.

-Audio verplaatst naar een (momenteel erg simpele) AudioManager door middel van een Event.

-Random variables een betere plek gegeven/verwijderd.

Ik heb nog geen tijd gehad om aan de Input te zitten, maar ik wil daar unity's nieuwe input system voor gebruiken.

