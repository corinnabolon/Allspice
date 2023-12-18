-- Active: 1702937297947@@34.211.60.37@3306@allspice

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title VARCHAR(255) NOT NULL,
        instructions VARCHAR(2000),
        img VARCHAR(1000) NOT NULL,
        category ENUM(
            "Cheese",
            "Soup",
            "Mexican",
            "Italian",
            "Specialty Coffee"
        ) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name VARCHAR(255) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS favorites(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        accountId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        UNIQUE(recipeId, accountId)
    ) default charset utf8 COMMENT '';

SELECT * FROM recipes;

SELECT * FROM accounts;

DROP TABLE favorites;

DROP TABLE recipes;

DROP TABLE ingredients;

INSERT INTO
    recipes(
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        "Strawberry Shortcake",
        "This is super complicated to make and needs a lot of ingredients.",
        "https://plus.unsplash.com/premium_photo-1669680785708-2c756ee97de8?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8c3RyYXdiZXJyeSUyMHNob3J0Y2FrZXxlbnwwfHwwfHx8MA%3D%3D",
        "Italian",
        "652ef11083c134acc79f1229"
    );

SELECT reci.*, acc.*
FROM recipes reci
    JOIN accounts acc ON reci.creatorId = acc.id
WHERE reci.id = 1;

DROP TABLE recipes;

SELECT reci.*, acc.*
FROM recipes reci
    JOIN accounts acc ON reci.creatorId = acc.id
WHERE reci.id = 1;

INSERT INTO
    ingredients (name, quantity, recipeId)
VALUES ("paprika", "2 tsp", "2");

SELECT * FROM ingredients WHERE id = 1;

SELECT * FROM ingredients;

SELECT * FROM favorites;