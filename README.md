# MyContact-API

Une API RESTful pour gérer les contacts des employés de l'entreprise Blocalimentations, développée avec ASP.NET Core 8.0 et Entity Framework Core.

## Description

MyContact-API est une application backend qui permet de gérer les informations des salariés, des services et des sites d'une entreprise. Elle offre une interface REST pour créer, lire, mettre à jour et supprimer des données relatives aux employés.

## Fonctionnalités

- Gestion des salariés (CRUD)
- Recherche de salariés par nom
- Filtrage des salariés par service ou par site
- Authentification utilisateur (inscription et connexion)

## Technologies Utilisées

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- MySQL (via Pomelo.EntityFrameworkCore.MySql)
- BCrypt.Net pour le hachage des mots de passe
- Swagger pour la documentation de l'API

## Prérequis

- .NET 8.0 SDK
- MySQL Server
- Un outil pour tester les API REST (Postman, Swagger UI, etc.)
- Serveur web [XAMPP](https://www.apachefriends.org/index.html) ou [WampServer](https://www.wampserver.com/)


## Installation

1. Clonez le dépôt
   ```
   git clone https://github.com/hanan3889/MyContact-API.git
   cd MyContact-API
   ```

2. Restaurez les packages NuGet
   ```
   dotnet restore
   ```

3. Configurez la base de données
   - Assurez-vous que MySQL est en cours d'exécution
   - Créez une base de données nommée `mycontactdb`
   - Exécutez le script SQL situé dans `/DB/MyContactDB.sql` pour créer les tables et insérer les données initiales

4. Mettez à jour la chaîne de connexion (si nécessaire)
   - Ouvrez `appsettings.json`
   - Modifiez la chaîne de connexion selon votre configuration MySQL

5. Exécutez l'application
   ```
   dotnet run
   ```

6. Accédez à Swagger UI
   ```
   http://localhost:5110/swagger
   ```

## Structure de la Base de Données

- **Sites**: Sites géographiques de l'entreprise
- **Services**: Départements ou services de l'entreprise
- **Salaries**: Informations des employés
- **Users**: Utilisateurs du système

## Points d'Accès (Endpoints) API

### Salariés

- `GET /api/Salaries/get/all` - Récupérer tous les salariés
- `GET /api/Salaries/get/{id}` - Récupérer un salarié par ID
- `GET /api/Salaries/get/name/{nom}` - Rechercher des salariés par nom
- `POST /api/Salaries/create` - Créer un nouveau salarié
- `PUT /api/Salaries/update/{id}` - Mettre à jour un salarié
- `DELETE /api/Salaries/delete/{id}` - Supprimer un salarié

### Services

- `GET /api/Services/get/name/{serviceName}` - Récupérer les salariés par service

### Sites

- `GET /api/Sites/get/name/{ville}` - Récupérer les salariés par ville

### Utilisateurs

- `POST /api/Users/register` - Inscrire un nouvel utilisateur
- `POST /api/Users/login` - Connecter un utilisateur
- `GET /api/Users` - Récupérer tous les utilisateurs
- `GET /api/Users/{email}` - Récupérer un utilisateur par email

## Exemples d'Utilisation

### Récupérer tous les salariés

```bash
curl -X GET "http://localhost:5110/api/Salaries/get/all" -H "accept: application/json"
```

### Créer un nouveau salarié

```bash
curl -X POST "http://localhost:5110/api/Salaries/create" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"nom\":\"Dupont\",\"prenom\":\"Jean\",\"telephoneFixe\":\"01-23-45-67-89\",\"telephonePortable\":\"06-12-34-56-78\",\"email\":\"jean.dupont@blocalimentation.fr\",\"serviceId\":1,\"siteId\":1}"
```

### Connexion utilisateur

```bash
curl -X POST "http://localhost:5110/api/Users/login" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"email\":\"admin@example.com\",\"password\":\"MonMotDePasse\"}"
```

## Développement

### Modèles de Données

- **Salaries**: Représente un employé avec ses informations de contact et ses associations
- **Services**: Représente un département ou service de l'entreprise
- **Sites**: Représente un site géographique de l'entreprise
- **Users**: Représente un utilisateur du système avec authentification
- **SalariesDto**: Objet de transfert de données pour les salariés, utilisé pour simplifier les réponses API

### Structure du Projet

- `/Controllers`: Contrôleurs API REST
- `/Models`: Modèles de données et DTOs
- `/DB`: Scripts SQL et configuration de la base de données

## Sécurité

- Les mots de passe sont hachés avec BCrypt avant d'être stockés en base de données
- Validation des entrées sur toutes les requêtes


