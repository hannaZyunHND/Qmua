/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 * customer: hiephv
 */

CKEDITOR.plugins.add('filemanager', {
    //requires: 'dialog',
    icons: 'filemanager', // %REMOVE_LINE_CORE%
    hidpi: true, // %REMOVE_LINE_CORE%
    init: function (editor) {
        var command = editor.addCommand('filemanager',
    {
        exec: function (editor) {


            function receiveData(el) {
                try {
                    $.each(el, function (i, v) {
                        editor.insertHtml('<img src="/' + StorageUrl + '/' + v + '"/>');
                    });
                } catch (el) {

                }
                // }
            }
            if (typeof window.addEventListener != 'undefined') {
                window.addEventListener('message', receiveData, false);
            }
            else if (typeof window.attachEvent != 'undefined') {
                window.attachEvent('onmessage', receiveData);
            }
            R.FileManager.Open(receiveData);
        }
    });
        editor.ui.addButton && editor.ui.addButton('filemanager', {
            label: 'File Manager',
            command: 'filemanager',
            toolbar: 'filemanager'

        });


    }
});
