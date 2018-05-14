EXECUTE core.create_app 'Frapid.WebsiteBuilder', 'Website', 'Website', '1.0', 'MixERP Inc.', 'December 1, 2015', 'world blue', '/dashboard/website/contents', null;

EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Tasks', 'Tasks', '', 'tasks icon', '';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Configuration', 'Configuration', '/dashboard/website/configuration', 'configure icon', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'ManageCategories', 'Manage Categories', '/dashboard/website/categories', 'sitemap icon', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'AddNewContent', 'Add a New Content', '/dashboard/website/contents/new', 'file', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'ViewContents', 'View Contents', '/dashboard/website/contents', 'desktop', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'AddNewBlogPost', 'Add a New Blog Post', '/dashboard/website/blogs/new', 'write', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'ViewBlogPosts', 'View Blog Posts', '/dashboard/website/blogs', 'browser', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Menus', 'Menus', '/dashboard/website/menus', 'star', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Contacts', 'Contacts', '/dashboard/website/contacts', 'phone', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Subscriptions', 'Subscriptions', '/dashboard/website/subscriptions', 'newspaper', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'LayoutManager', 'Layout Manager', '/dashboard/website/layouts', 'grid layout', '';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'EmailTemplates', 'Email Templates', '', 'mail', '';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'SubscriptionAdded', 'Subscription Added', '/dashboard/website/subscription/welcome', 'plus circle', 'Email Templates';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'SubscriptionRemoved', 'Subscription Removed', '/dashboard/website/subscription/removed', 'minus circle', 'Email Templates';

GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Content Editor', 
@office_id, 
'Frapid.WebsiteBuilder',
'{Tasks, Manage Categories, Add New Content, View Contents}';

EXECUTE auth.create_app_menu_policy
'User', 
@office_id, 
'Frapid.WebsiteBuilder',
'{
    Tasks, Manage Categories, Add New Content, View Contents, 
    Menus, Contacts, Subscriptions, 
    Layout Manager, Edit Master Layout (Homepage), Edit Master Layout, Edit Header, Edit Footer, 404 Not Found Document
}';


EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.WebsiteBuilder',
'{*}';



GO
