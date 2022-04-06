CKEDITOR.editorConfig = function (config) {
    config.language = 'vi';
    config.allowedContent = true;
    config.removePlugins = 'base64Image,image2';	
    // config.uiColor = '#AADC6E';
    //config.filebrowserBrowseUrl = '~/CMS/Modules/FileManager/Main.aspx';
  //  config.extraPlugins = 'filemanager,autogrow';
 //   config.extraPlugins = 'filemanager';
    // Toolbar configuration generated automatically by the editor based on config.toolbarGroups.

    // Remove some buttons provided by the standard plugins, which are
    // not needed in the Standard(s) toolbar.
    config.removeButtons = 'Underline,Subscript,Superscript';

    // Set the most common block elements.
    config.format_tags = 'p;h1;h2;h3;pre';

    // Simplify the dialog windows.
    config.removeDialogTabs = 'image:advanced;link:advanced';

    //config.autoGrow_minHeight = 200;
    //config.autoGrow_bottomSpace = 50;
    //config.autoGrow_onStartup = true;
};




