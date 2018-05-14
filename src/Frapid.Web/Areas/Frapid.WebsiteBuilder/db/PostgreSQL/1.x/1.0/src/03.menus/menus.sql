SELECT * FROM core.create_app('Frapid.WebsiteBuilder', 'Website', 'Website', '1.0', 'MixERP Inc.', 'December 1, 2015', 'world blue', '/dashboard/website/contents', null);

SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Tasks', 'Tasks', '', 'tasks icon', '');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Configuration', 'Configuration', '/dashboard/website/configuration', 'configure icon', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'ManageCategories', 'Manage Categories', '/dashboard/website/categories', 'sitemap icon', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'AddNewContent', 'Add a New Content', '/dashboard/website/contents/new', 'file', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'ViewContents', 'View Contents', '/dashboard/website/contents', 'desktop', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'AddNewBlogPost', 'Add a New Blog Post', '/dashboard/website/blogs/new', 'write', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'ViewBlogPosts', 'View Blog Posts', '/dashboard/website/blogs', 'browser', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Menus', 'Menus', '/dashboard/website/menus', 'star', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Contacts', 'Contacts', '/dashboard/website/contacts', 'phone', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Subscriptions', 'Subscriptions', '/dashboard/website/subscriptions', 'newspaper', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'LayoutManager', 'Layout Manager', '/dashboard/website/layouts', 'grid layout', '');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'EmailTemplates', 'Email Templates', '', 'mail', '');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'SubscriptionAdded', 'Subscription Added', '/dashboard/website/subscription/welcome', 'plus circle', 'Email Templates');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'SubscriptionRemoved', 'Subscription Removed', '/dashboard/website/subscription/removed', 'minus circle', 'Email Templates');


SELECT * FROM auth.create_app_menu_policy
(
    'Content Editor', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.WebsiteBuilder',
    '{Tasks, Manage Categories, Add New Content, View Contents}'::text[]
);

SELECT * FROM auth.create_app_menu_policy
(
    'User', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.WebsiteBuilder',
    '{
        Tasks, Manage Categories, Add New Content, View Contents, 
        Menus, Contacts, Subscriptions, 
        Layout Manager, Edit Master Layout (Homepage), Edit Master Layout, Edit Header, Edit Footer, 404 Not Found Document
    }'::text[]
);


SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.WebsiteBuilder',
    '{*}'::text[]
);
