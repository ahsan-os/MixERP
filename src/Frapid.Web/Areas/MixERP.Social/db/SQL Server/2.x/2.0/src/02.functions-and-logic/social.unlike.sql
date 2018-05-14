IF OBJECT_ID('social.unlike') IS NOT NULL
DROP PROCEDURE social.unlike;

GO

CREATE PROCEDURE social.unlike(@user_id integer, @feed_id bigint)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    IF EXISTS
    (
        SELECT 0 FROM social.liked_by
        WHERE social.liked_by.feed_id = @feed_id
        AND  social.liked_by.liked_by = @user_id
    )
	BEGIN
        UPDATE social.liked_by
        SET 
            unliked     = 1,
            unliked_on  = getutcdate()
        WHERE social.liked_by.feed_id = @feed_id
        AND social.liked_by.liked_by = @user_id;
    END;
END;

GO
