IF OBJECT_ID('social.like') IS NOT NULL
DROP PROCEDURE social."like";

GO

CREATE PROCEDURE social."like"(@user_id integer, @feed_id bigint)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    IF NOT EXISTS
    (
        SELECT 0 
        FROM social.liked_by
        WHERE social.liked_by.feed_id = @feed_id
        AND social.liked_by.liked_by = @user_id
    )
	BEGIN
        INSERT INTO social.liked_by(feed_id, liked_by)
        SELECT @feed_id, @user_id;
    END;

    IF EXISTS
    (
        SELECT 0 
        FROM social.liked_by
        WHERE social.liked_by.feed_id = @feed_id
        AND social.liked_by.liked_by = @user_id
        AND social.liked_by.unliked = 1
    )
	BEGIN
        UPDATE social.liked_by
        SET
            unliked     = 0,
            unliked_on  = NULL
        WHERE social.liked_by.feed_id = @feed_id
        AND social.liked_by.liked_by = @user_id;
    END;
END;

GO
