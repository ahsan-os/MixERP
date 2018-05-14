DELETE FROM website.contents;
DELETE FROM website.menu_items;
DELETE FROM website.menus;

INSERT INTO website.menus(menu_name)
SELECT 'Default';

INSERT INTO website.menu_items(menu_id, sort, title, url, target)
SELECT 1, 1, 		'Home', 		'/', '' 						UNION ALL
SELECT 1, 100, 		'Sign Up', 		'/account/sign-up', '_parent' 	UNION ALL
SELECT 1, 1000, 	'Admin Area', 	'/dashboard', '_parent' 		UNION ALL
SELECT 1, 10000,	'Contact Us', 	'/contact-us', '';

INSERT INTO website.categories(category_name, alias, seo_description)
SELECT 'Default', 'default', '';

INSERT INTO website.categories(category_name, alias, seo_description)
SELECT 'Legal', 'legal', '';

DELETE FROM website.contents;

INSERT INTO website.contents(title, tags, alias, category_id, publish_on, is_draft, seo_description, is_homepage, contents)
SELECT 'Welcome to Frapid', 'frapid,cms', 'welcome-to-frapid', website.get_category_id_by_category_alias('default'), NOW(), false, 'Homepage of Frapid Framework', true, '<div class="ui basic inverted attached segment" id="banner">
    <div class="ui caption container">
        <div class="ui huge inverted header">
            Frapid Framework
        </div>
        <p>
            Howdy, your Frapid instance is now up and running.
            <br/> Login to your admin area now and start building your website.
            <p>
            <div class="ui hidden divider"></div>
            <div class="ui small inverted buttons">
                <a class="ui inverted button" href="/dashboard">Admin Area</a>
                <a class="ui inverted button" href="http://docs.frapid.com/" target="_blank">Read Documentation</a>
            </div>
    </div>
</div>
<div class="ui attached vertically padded segment">
    <div class="ui story container">
        <div class="ui huge header">
            <span>Learn More</span> About Frapid
        </div>
        <div class="ui divider"></div>
        <p>
            Frapid is a multi-tenant application development framework released under MIT License.
            <br/> Learn more about Frapid and quickly add contents in your website.
        </p>
    </div>



    <div class="ui three column page stackable doubling padded grid">
        <div class="column">
            <div class="ui ad box grid">
                <div class="ad icon four wide column">
                    <i class="circular level up inverted red icon"></i>
                </div>
                <div class="ad details twelve wide column">
                    <div class="ui red header">Edit Header</div>
                    <p>
                        Read the <a href="http://docs.frapid.com/" target="_blank">documentation</a> on editing your site header.
                    </p>
                </div>
            </div>
        </div>
        <div class="column">
            <div class="ui ad box grid">
                <div class="ad icon four wide column">
                    <i class="circular level down inverted violet icon"></i>
                </div>
                <div class="ad details twelve wide column">
                    <div class="ui violet header">Edit Footer</div>
                    <p>
                        Edit the contents and links in your
                        <a href="http://docs.frapid.com/site/footer" target="_blank">site footer</a>.
                    </p>
                </div>
            </div>
        </div>
        <div class="column">
            <div class="ui ad box grid">
                <div class="ad icon four wide column">
                    <i class="circular checkmark inverted teal icon"></i>
                </div>
                <div class="ad details twelve wide column">
                    <div class="ui teal header">Manage Pages</div>
                    <p>
                        Quickly add site pages, edit, and
                        <a href="http://docs.frapid.com/site/contents" target="_blank">manage them</a>.
                    </p>
                </div>
            </div>
        </div>
        <div class="column">
            <div class="ui ad box grid">
                <div class="ad icon four wide column">
                    <i class="circular linkify inverted green icon"></i>
                </div>
                <div class="ad details twelve wide column">
                    <div class="ui green header">Menu Builder</div>
                    <p>
                        Learn how you can add & edit
                        <a href="http://docs.frapid.com/site/menus" target="_blank">site menus</a>.
                    </p>
                </div>
            </div>
        </div>
        <div class="column">
            <div class="ui ad box grid">
                <div class="ad icon four wide column">
                    <i class="circular code inverted blue icon"></i>
                </div>
                <div class="ad details twelve wide column">
                    <div class="ui blue header">Develop Frapid Apps</div>
                    <p>
                        Develop your own open source or commercial
                        <a href="http://docs.frapid.com/site/develop-frapid-app" target="_blank">frapid apps</a>.
                    </p>
                </div>
            </div>
        </div>
        <div class="column">
            <div class="ui ad box grid">
                <div class="ad icon four wide column">
                    <i class="circular line inverted yellow chart icon"></i>
                </div>
                <div class="ad details twelve wide column">
                    <div class="ui yellow header">Improve Frapid</div>
                    <p>
                        Improve Frapid by
                        <a href="https://github.com/frapid/frapid/issues/new" target="_blank">documenting</a> and fixing bugs.
                    </p>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="ui attached brown vertically padded segment" id="contact">
    <div class="ui story container">
        <div class="ui huge red header">
            Keep in Touch
        </div>
        <div class="ui hidden divider"></div>
        <p>
            Want us to build an application or website for you?
            <br/> Why not contact us today? We will be happy to listen to you!
        </p>
        <div class="ui hidden divider"></div>
        <p>
            <a class="ui orange button" href="/contact-us">Contact Us</a>
        </p>
    </div>
</div>';

    
INSERT INTO website.contents(title, tags, alias, category_id, publish_on, is_draft, seo_description, is_homepage, markdown, contents)
SELECT 'Terms of Use', '', 'terms-of-use', website.get_category_id_by_category_alias('legal'), NOW(), false, 'Terms of Use', false, '# Terms of Use

This document is empty.', '<h1 id="terms-of-use">Terms of Use</h1>
<p>This document is empty.</p>';

INSERT INTO website.contents(title, tags, alias, category_id, publish_on, is_draft, seo_description, is_homepage, markdown, contents)
SELECT 'Privacy Policy', '', 'privacy-policy', website.get_category_id_by_category_alias('legal'), NOW(), false, 'Privacy Policy', false, '# Privacy Policy

This document is empty.', '<h1 id="privacy-policy">Privacy Policy</h1>
<p>This document is empty.</p>';


INSERT INTO website.contacts(title, name, "position", address, city, state, country, postal_code, telephone, details, email, display_email, display_contact_form, status)
SELECT 'Corporate Headquarters', 'Your Office Name', '', 'Address', 'City', 'State', 'Country', '000', '000 000 000', '', 'info@frapid.com', false, true, true UNION ALL
SELECT 'United States', 'John Doe', 'Client Partner', '', '', '', '', '', '', 'Texas', 'info@frapid.com', false, true, true;