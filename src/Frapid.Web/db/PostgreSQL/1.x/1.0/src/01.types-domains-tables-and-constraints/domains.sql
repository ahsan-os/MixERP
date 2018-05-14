DROP DOMAIN IF EXISTS public.money_strict CASCADE;
CREATE DOMAIN public.money_strict
AS numeric(30, 6)
CHECK
(
    VALUE > 0
);


DROP DOMAIN IF EXISTS public.money_strict2 CASCADE;
CREATE DOMAIN public.money_strict2
AS numeric(30, 6)
CHECK
(
    VALUE >= 0
);

DROP DOMAIN IF EXISTS public.integer_strict CASCADE;
CREATE DOMAIN public.integer_strict
AS integer
CHECK
(
    VALUE > 0
);

DROP DOMAIN IF EXISTS public.integer_strict2 CASCADE;
CREATE DOMAIN public.integer_strict2
AS integer
CHECK
(
    VALUE >= 0
);

DROP DOMAIN IF EXISTS public.smallint_strict CASCADE;
CREATE DOMAIN public.smallint_strict
AS smallint
CHECK
(
    VALUE > 0
);

DROP DOMAIN IF EXISTS public.smallint_strict2 CASCADE;
CREATE DOMAIN public.smallint_strict2
AS smallint
CHECK
(
    VALUE >= 0
);

DROP DOMAIN IF EXISTS public.decimal_strict CASCADE;
CREATE DOMAIN public.decimal_strict
AS numeric(30, 6)
CHECK
(
    VALUE > 0
);

DROP DOMAIN IF EXISTS public.decimal_strict2 CASCADE;
CREATE DOMAIN public.decimal_strict2
AS numeric(30, 6)
CHECK
(
    VALUE >= 0
);

DROP DOMAIN IF EXISTS public.color CASCADE;
CREATE DOMAIN public.color
AS text;

DROP DOMAIN IF EXISTS public.photo CASCADE;
CREATE DOMAIN public.photo
AS text;


DROP DOMAIN IF EXISTS public.html CASCADE;
CREATE DOMAIN public.html
AS text;

DROP DOMAIN IF EXISTS public.password CASCADE;
CREATE DOMAIN public.password
AS text;