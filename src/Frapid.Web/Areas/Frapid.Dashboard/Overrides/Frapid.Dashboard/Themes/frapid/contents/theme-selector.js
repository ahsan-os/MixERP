function setTheme(el){
    function request(theme){
        var url = "/dashboard/my/themes/set-default/";
        url += theme;
        return window.getAjaxRequest(url, "POST");
    };

    const theme = $(el).text();
    if(!theme){
        return;
    };
    
    const ajax = request(theme);
    
    ajax.success(function(){
        window.location = window.location;
    });        
};

function loadThemes(){
    function append(theme){
        const item = $("<a onclick='setTheme(this);' class='item' />");
        item.text(theme);
        $(".theme.selector .scrolling.menu").append(item);
    };
    
    function request() {
        const url = "/dashboard/my/themes";
        return window.getAjaxRequest(url);
    };
    
    const ajax = request();
    
    ajax.success(function(response){
        $.each(response, function(){
            append(this);
        });
    });
};

loadThemes();
