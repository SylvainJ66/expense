create table expenses
(
    "id"        uuid    not null
        constraint "PK_expenses"
            primary key,
    "user_id" uuid not null,
    "expense_date" timestamp not null,
    "created_at"   timestamp    not null,
    "expense_type"     text not null,
    "amount"     numeric not null,
    "currency"     text not null,
    "comment"     text not null
);

create table users
(
    "id"        uuid    not null
        constraint "PK_users"
            primary key,
    "first_name" text not null,
    "name" text not null,
    "currency" text not null
);
