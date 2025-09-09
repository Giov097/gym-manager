CREATE TABLE GymManager.dbo.users
(
    id         bigint IDENTITY (1,1) NOT NULL,
    first_name varchar(255)          NULL,
    last_name  varchar(255)          NULL,
    email      varchar(255)          NOT NULL,
    password   varchar(255)          NOT NULL,
    active     bit                   NOT NULL DEFAULT 1,
    CONSTRAINT users_pk PRIMARY KEY (id),
    CONSTRAINT users_email UNIQUE (email)
);

CREATE TABLE GymManager.dbo.fees
(
    id         bigint IDENTITY (1,1) NOT NULL,
    start_date date                  NOT NULL,
    end_date   date                  NOT NULL,
    amount     decimal(38, 0)        NOT NULL,
    user_id    bigint                NOT NULL,
    CONSTRAINT fees_pk PRIMARY KEY (id),
    CONSTRAINT fees_users_FK FOREIGN KEY (user_id) REFERENCES GymManager.dbo.users (id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE GymManager.dbo.payments
(
    id             bigint IDENTITY (1,1) NOT NULL,
    fee_id         bigint                NOT NULL,
    amount         decimal(38, 0)        NOT NULL,
    payment_date   date                  NOT NULL,
    payment_method varchar(50)           NOT NULL,
    status         varchar(20)           NOT NULL,
    card_last4     char(4)               NULL,
    card_brand     varchar(50)           NULL,
    receipt_number varchar(100)          NULL,
    CONSTRAINT payments_pk PRIMARY KEY (id),
    CONSTRAINT payments_fees_FK FOREIGN KEY (fee_id) REFERENCES GymManager.dbo.fees (id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE GymManager.dbo.roles
(
    id        bigint IDENTITY (1,1) NOT NULL,
    role_name varchar(50)           NOT NULL,
    CONSTRAINT roles_pk PRIMARY KEY (id),
    CONSTRAINT roles_role_name UNIQUE (role_name)
);

CREATE TABLE GymManager.dbo.user_roles
(
    user_id bigint NOT NULL,
    role_id bigint NOT NULL,
    CONSTRAINT user_roles_pk PRIMARY KEY (user_id, role_id),
    CONSTRAINT user_roles_users_FK FOREIGN KEY (user_id) REFERENCES GymManager.dbo.users (id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT user_roles_roles_FK FOREIGN KEY (role_id) REFERENCES GymManager.dbo.roles (id) ON DELETE CASCADE ON UPDATE CASCADE
);

INSERT INTO GymManager.dbo.roles (role_name)
VALUES (N'STUDENT');
INSERT INTO GymManager.dbo.roles (role_name)
VALUES (N'TRAINER');
INSERT INTO GymManager.dbo.roles (role_name)
VALUES (N'ADMIN');

