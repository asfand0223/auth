CREATE TABLE users (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    username character varying(255) NOT NULL,
    password character varying(255) NOT NULL,
    created_on timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE refresh_tokens (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    user_id uuid NOT NULL,
    expires_at timestamp with time zone NOT NULL
);

