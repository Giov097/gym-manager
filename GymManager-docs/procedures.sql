IF TYPE_ID(N'dbo.StringList') IS NULL
CREATE TYPE dbo.StringList AS TABLE
(
    Value NVARCHAR(255) NOT NULL
);
GO

IF TYPE_ID(N'dbo.IntList') IS NULL
CREATE TYPE dbo.IntList AS TABLE
(
    Value INT NOT NULL
);
GO

CREATE OR ALTER PROCEDURE dbo.usp_CreateUser @FirstName NVARCHAR(100),
                                             @LastName NVARCHAR(100),
                                             @Email NVARCHAR(255),
                                             @Password NVARCHAR(255),
                                             @NewId BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO users (first_name, last_name, email, password)
    VALUES (@FirstName, @LastName, @Email, @Password);
    SET @NewId = CONVERT(BIGINT, SCOPE_IDENTITY());
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetRolesByNames @Names dbo.StringList READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM roles WHERE role_name IN (SELECT Value FROM @Names);
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_InsertUserRole @UserId BIGINT,
                                                 @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO user_roles (user_id, role_id) VALUES (@UserId, @RoleId);
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_InsertUserRolesBulk @UserId BIGINT,
                                                      @RoleIds dbo.IntList READONLY
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO user_roles (user_id, role_id)
    SELECT @UserId, Value
    FROM @RoleIds;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_DeleteUserRoles @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM user_roles WHERE user_id = @UserId;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetUserById @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.id,
           u.first_name,
           u.last_name,
           u.email,
           u.password,
           r.role_name,
           f.id      AS fee_id,
           f.start_date,
           f.end_date,
           f.amount,
           f.user_id AS fee_user_id,
           p.id      AS payment_id,
           p.amount  AS payment_amount,
           p.payment_date,
           p.payment_method,
           p.status  AS payment_status,
           p.card_last4,
           p.card_brand,
           p.receipt_number,
           r.role_name
    FROM users u
             LEFT JOIN user_roles ur ON u.id = ur.user_id
             LEFT JOIN roles r ON ur.role_id = r.id
             LEFT JOIN fees f ON u.id = f.user_id
             LEFT JOIN payments p ON f.id = p.fee_id
    WHERE u.id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetUserByEmail @Email NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM users u
             LEFT JOIN user_roles ur ON u.id = ur.user_id
             LEFT JOIN roles r ON ur.role_id = r.id
             LEFT JOIN fees f ON u.id = f.user_id
             LEFT JOIN payments p ON f.id = p.fee_id
    WHERE u.email = @Email;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetUserByFeeId @FeeId BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.*, r.role_name
    FROM users u
             INNER JOIN fees f ON u.id = f.user_id
             LEFT JOIN user_roles ur ON u.id = ur.user_id
             LEFT JOIN roles r ON ur.role_id = r.id
    WHERE f.id = @FeeId;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetAllUsers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.id      AS user_id,
           u.first_name,
           u.last_name,
           u.email,
           u.password,
           r.role_name,
           f.id      AS fee_id,
           f.start_date,
           f.end_date,
           f.amount,
           f.user_id AS fee_user_id,
           p.id      AS payment_id,
           p.amount  AS payment_amount,
           p.payment_date,
           p.payment_method,
           p.status  AS payment_status,
           p.card_last4,
           p.card_brand,
           p.receipt_number
    FROM users u
             LEFT JOIN user_roles ur ON u.id = ur.user_id
             LEFT JOIN roles r ON ur.role_id = r.id
             LEFT JOIN fees f ON u.id = f.user_id
             LEFT JOIN payments p ON f.id = p.fee_id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_DeleteUser @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM users WHERE id = @Id;
    SELECT @@ROWCOUNT AS RowsAffected;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_UpdateUser @Id BIGINT,
                                             @FirstName NVARCHAR(100),
                                             @LastName NVARCHAR(100),
                                             @Email NVARCHAR(255),
                                             @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE users
    SET first_name = @FirstName,
        last_name  = @LastName,
        email      = @Email,
        password   = @Password
    WHERE id = @Id;
    SELECT @@ROWCOUNT AS RowsAffected;
END;
GO

CREATE OR ALTER PROCEDURE sp_CreateFee @Amount decimal(18, 2),
                                       @StartDate date,
                                       @EndDate date,
                                       @UserId bigint
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO fees (amount, start_date, end_date, user_id)
    VALUES (@Amount, @StartDate, @EndDate, @UserId);

    SELECT SCOPE_IDENTITY() AS NewId;
END
GO

CREATE OR ALTER PROCEDURE sp_GetFeeById @Id bigint
AS
BEGIN
    SET NOCOUNT ON;
    SELECT f.id     AS [fee_id],
           f.start_date,
           f.end_date,
           f.amount AS [fee_amount],
           f.user_id,
           p.id     AS [payment_id],
           p.amount AS [payment_amount],
           p.payment_date,
           p.payment_method,
           p.status,
           p.card_last4,
           p.card_brand,
           p.receipt_number
    FROM fees f
             FULL OUTER JOIN payments p ON f.id = p.fee_id
    WHERE f.id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_GetAllFees
AS
BEGIN
    SET NOCOUNT ON;
    SELECT f.id     AS [fee_id],
           f.start_date,
           f.end_date,
           f.amount AS [fee_amount],
           f.user_id,
           p.id     AS [payment_id],
           p.amount AS [payment_amount],
           p.payment_date,
           p.payment_method,
           p.status,
           p.card_last4,
           p.card_brand,
           p.receipt_number
    FROM fees f
             FULL OUTER JOIN payments p ON f.id = p.fee_id;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateFee @Id bigint,
                                       @Amount decimal(18, 2),
                                       @StartDate date,
                                       @EndDate date
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE fees
    SET amount     = @Amount,
        start_date = @StartDate,
        end_date   = @EndDate
    WHERE id = @Id;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteFee @Id bigint
AS
BEGIN
    SET
        NOCOUNT ON;

    DELETE
    FROM fees
    WHERE id = @Id;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
