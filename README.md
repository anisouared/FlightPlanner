# FlightPlanner

- L'application FlightPlanner contient toutes les fonctionnalités demandées dans l'énoncé avec un espace membre en bonus.
- Durèes de developpement : 20h - 2 jours et demi (Entre deplacements à l'etranger et déménagement :))

========================================================================================================================================

Techno-Back : ASP.NET Core 2.1 (MVC)
Techno-Front : Bootstrap v3.3.7, Html, Css
Base de donnée : SQL Server
IDE : Visual Studio Code

========================================================================================================================================

# Architecture Base de donnée :
- Table [user] : Stocke les informations des utilisateurs autorisés à utiliser FlightPlanner (après enregistrement sur l'appli).
- Table  [airport] : Stocke les informations de localisations, code ..etc de tous les aeroports du monde.
- Table [aircraft] : Stocke les informations sur le modèle, la consommation des avions par distance de 1000km + la consommation du kérosène
lors du decolage pour chaque modèle d'avion.
- Table [flight] : Stocke les vols rajoutés par les utilisateurs de l'appli (aeroports de depart et d'arriver, l'id de utilisateur qui a rajouté le vol,
la distance parcourue et le kérosène consommé(takeoff + trajet)  )


# Architecture Applicative :
-BusinessEntities : Une bibliothèque contenant les modèles utilisés pour le transfert des données entre les differentes couches + validation.
-DataAccessLayer : La couche permettant d'acceder aux données (Ajout, modification ...) à travers l'ORM EntityFrameWorkCore et aussi des class de mapping 
pour mapper les modèles d'EF avec les modèles de la librairie BusinessEntities (DTOs).
-BusinessLogic : Cette couche comporte des class avec de la logique métier, qui nous permettent de communiquer avec la couche DataAccess
-UI : L'application ASP.NET Core qui comporte les controlleurs qui font les traitements de données necessaire ainsi que la logique de l'application 
notamment celle demandé sur l'énoncé (des commentaires sont dans le code pour expliquer cette partie - class FlightController) 

========================================================================================================================================
# POUR COMPILER :

-Il faut d'abord executer le fichier FlightPlannerDB.sql dans SQL Serveur pour mettre en place la base de données de l'appli incluant la données des
aeroport, les modèles d'avions et tout ce dont on aura besoin pour utiliser l'application.
-Ensuite, il faut ouvrir le fichier FlightPlannerWorkSpace.code-workspace avec Visual Studio Code
-Après il faut modifier les fichiers Configs (appsetting.json & appsetting.Development.json) pour mettre le chemin de la base de données installée,
le chemin du fichier log de l'appli et les informations de l'utilisateur par defaut de l'application
-AU FINAL IL FAUT LANCER LA COMPILATION !

=======================================================================================================================================================================
# Utilisation de FlightPlanner :
- Il faut d'abord s'enregistrer pour pouvoir rajouter des vols.
- Chaque utilisateur peut voir la liste des vols qu'il a ajouté et aussi les modifiers.
- Tout les utilisateurs authentifiés peuvent acceder à une la liste de tout les vols contenant les distances ainsi que les consommations du kérosène

# REMARQUE IMPORTANTE : Pour ajouter des vols il faut utiliser les codes des aeroport (exemple : LYS pour l'aeroport de Lyon, CDG pour l'aeroport de Charles De Gaulle )

=======================================================================================================================================================================
# Améliorations (Si j'avais plus de temps) :
- Commenter le code.
- Mise en place de tests unitaires.
- Enrichir l'espace membre (Roles ...etc)
- Plus de securité (Protection contre CSRF ..etc)
- Plus de validation au niveau des DTO (DataAnnotations)
- Requetes AJAX
- Ameliorer le front en utilisant des scripts JavaScripts ou bien à l'aide d'un framework type : Angluar, React ...etc




