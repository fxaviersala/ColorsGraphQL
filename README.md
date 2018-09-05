# Colors GraphQL API

Implementació de GraphQL per una API de traducció de colors. Si s'envia la petició amb "color", es poden extreure totes les dades que calguin d'un color: RGB, Traduccions, etc...

![Exemple](README/out.gif)

També es pot veure una traducció individualment si la petició té d'arrel "translation" i li passem l'id

![Exemple](README/traduccions.png)

I es poden llistar tots els colors de la base de dades amb l'arrel "colors":

![Llista](README/tots-colors1.png)

Evidentment es poden recuperar les dades que calguin de cada color només especificant-les en la petició:

![Llista](README/totscolors2.png)

## Alies

Es poden obtenir diversos elements del servei fent servir àlies (bàsicament per evitar que hi hagi repeticions en els camps)

![àlies](README/alies.png)

## Fragments

També és pot obtenir el mateix evitant repetir les dades amb els fragments

![fragments](README/fragments.png)

## Variables

Es poden fer servir variables per reaprofitar les peticions que s'han fet i només especificar el que canvia

![variables](README/variables.png)

![variables2](README/variables2.png)

## Directives

En GraphQL hi ha una sèrie de directives per control·lar el funcionament. Per exemple `@include` permet definir si una part ha de sortir o no

![Directives](README/directives.png)
