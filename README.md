## Architecture

Le projet utilise CQRS. Il est donc découpé en 2 parties, le read et le write.
Le read est une api qui permet de lire les données, le write est une api qui permet de modifier les données.
Le projet est découpé en 3 couches, 
- Api, 
- Domain ou Core où l'on trouve la logique métier
- Infrastructure.

## Methods
Le TDD a été utilisé pour le developpement des couches domain et Core.
Des tests d'intégrations ont été ajoutés coté write pour tester les repository.

## Pour lancer le projet

lancer docker desktop avec postgresql
saisir les chaines de connexion dans les fichiers appsettings.json
executer les migrations (avec son IDE)
Coté read et coté write, il y a un fichier .http, qui permet de lancer les requetes http
Si pas possible depuis votre ide, ouvrir postman et importer les swaggers (read et write)

## tests d'intégration

les tests d'intégrations utilisent testcontainers, il faudra juste avoir un docker desktop lancé

# expense

Use clean archi, tdd
