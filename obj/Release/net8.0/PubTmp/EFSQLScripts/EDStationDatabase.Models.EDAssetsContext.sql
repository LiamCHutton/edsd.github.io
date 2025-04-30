IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [Allegiances] (
        [AllegianceId] int NOT NULL IDENTITY,
        [AllegianceName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Allegiances] PRIMARY KEY ([AllegianceId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [Economies] (
        [EconomyId] int NOT NULL IDENTITY,
        [EconomyName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Economies] PRIMARY KEY ([EconomyId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [StationType] (
        [StationTypeId] int NOT NULL IDENTITY,
        [StationTypeName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_StationType] PRIMARY KEY ([StationTypeId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [Superpowers] (
        [SuperpowerId] int NOT NULL IDENTITY,
        [SuperpowerName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Superpowers] PRIMARY KEY ([SuperpowerId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE TABLE [StationBookmarks] (
        [StationBookmarkId] int NOT NULL IDENTITY,
        [StationName] nvarchar(max) NOT NULL,
        [SystemName] nvarchar(max) NOT NULL,
        [EconomyId] int NULL,
        [AllegianceId] int NULL,
        [StationTypeId] int NULL,
        [SuperpowerId] int NULL,
        CONSTRAINT [PK_StationBookmarks] PRIMARY KEY ([StationBookmarkId]),
        CONSTRAINT [FK_StationBookmarks_Allegiances_AllegianceId] FOREIGN KEY ([AllegianceId]) REFERENCES [Allegiances] ([AllegianceId]),
        CONSTRAINT [FK_StationBookmarks_Economies_EconomyId] FOREIGN KEY ([EconomyId]) REFERENCES [Economies] ([EconomyId]),
        CONSTRAINT [FK_StationBookmarks_StationType_StationTypeId] FOREIGN KEY ([StationTypeId]) REFERENCES [StationType] ([StationTypeId]),
        CONSTRAINT [FK_StationBookmarks_Superpowers_SuperpowerId] FOREIGN KEY ([SuperpowerId]) REFERENCES [Superpowers] ([SuperpowerId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AllegianceId', N'AllegianceName') AND [object_id] = OBJECT_ID(N'[Allegiances]'))
        SET IDENTITY_INSERT [Allegiances] ON;
    EXEC(N'INSERT INTO [Allegiances] ([AllegianceId], [AllegianceName])
    VALUES (1, N''Aisling Duval''),
    (2, N''Arissa Lavingy-Duval''),
    (3, N''Denton Patreus''),
    (4, N''Zemina Torval''),
    (5, N''Jerome Archer''),
    (6, N''Felicia Winters''),
    (7, N''Edmund Mahon''),
    (8, N''Nakato Kaine''),
    (9, N''Archon Delaine''),
    (10, N''Li Yong-Rui''),
    (11, N''Parnav Antal''),
    (12, N''Yuri Grom''),
    (13, N''Independent''),
    (14, N''Pilot''''s Federation'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AllegianceId', N'AllegianceName') AND [object_id] = OBJECT_ID(N'[Allegiances]'))
        SET IDENTITY_INSERT [Allegiances] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EconomyId', N'EconomyName') AND [object_id] = OBJECT_ID(N'[Economies]'))
        SET IDENTITY_INSERT [Economies] ON;
    EXEC(N'INSERT INTO [Economies] ([EconomyId], [EconomyName])
    VALUES (1, N''Agriculture''),
    (2, N''Colony''),
    (3, N''Extraction''),
    (4, N''High Tech''),
    (5, N''Industrial''),
    (6, N''Military''),
    (7, N''Refinery''),
    (8, N''Service''),
    (9, N''Terraforming''),
    (10, N''Tourism''),
    (11, N''Prison Colony'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EconomyId', N'EconomyName') AND [object_id] = OBJECT_ID(N'[Economies]'))
        SET IDENTITY_INSERT [Economies] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StationTypeId', N'StationTypeName') AND [object_id] = OBJECT_ID(N'[StationType]'))
        SET IDENTITY_INSERT [StationType] ON;
    EXEC(N'INSERT INTO [StationType] ([StationTypeId], [StationTypeName])
    VALUES (1, N''Coriolis''),
    (2, N''Orbis''),
    (3, N''Ocellus''),
    (4, N''Outpost''),
    (5, N''Planetary Outpost''),
    (6, N''Planetary Port''),
    (7, N''Planetary Surface Port''),
    (8, N''Planetary Settlement''),
    (9, N''Asteroid Base'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StationTypeId', N'StationTypeName') AND [object_id] = OBJECT_ID(N'[StationType]'))
        SET IDENTITY_INSERT [StationType] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SuperpowerId', N'SuperpowerName') AND [object_id] = OBJECT_ID(N'[Superpowers]'))
        SET IDENTITY_INSERT [Superpowers] ON;
    EXEC(N'INSERT INTO [Superpowers] ([SuperpowerId], [SuperpowerName])
    VALUES (1, N''Empire''),
    (2, N''Federation''),
    (3, N''Alliance''),
    (4, N''Independent'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SuperpowerId', N'SuperpowerName') AND [object_id] = OBJECT_ID(N'[Superpowers]'))
        SET IDENTITY_INSERT [Superpowers] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StationBookmarkId', N'AllegianceId', N'EconomyId', N'StationName', N'StationTypeId', N'SuperpowerId', N'SystemName') AND [object_id] = OBJECT_ID(N'[StationBookmarks]'))
        SET IDENTITY_INSERT [StationBookmarks] ON;
    EXEC(N'INSERT INTO [StationBookmarks] ([StationBookmarkId], [AllegianceId], [EconomyId], [StationName], [StationTypeId], [SuperpowerId], [SystemName])
    VALUES (1, 13, 7, N''Low City'', 3, NULL, N''HR 1980''),
    (2, 10, 4, N''Ray Gateway'', 1, NULL, N''Diaguandri''),
    (3, 13, 6, N''Copernicus Observatory'', 1, NULL, N''Asterope'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StationBookmarkId', N'AllegianceId', N'EconomyId', N'StationName', N'StationTypeId', N'SuperpowerId', N'SystemName') AND [object_id] = OBJECT_ID(N'[StationBookmarks]'))
        SET IDENTITY_INSERT [StationBookmarks] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [IX_StationBookmarks_AllegianceId] ON [StationBookmarks] ([AllegianceId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [IX_StationBookmarks_EconomyId] ON [StationBookmarks] ([EconomyId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [IX_StationBookmarks_StationTypeId] ON [StationBookmarks] ([StationTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    CREATE INDEX [IX_StationBookmarks_SuperpowerId] ON [StationBookmarks] ([SuperpowerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415165041_init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250415165041_init', N'9.0.4');
END;

COMMIT;
GO

