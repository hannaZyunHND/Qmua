import Vue from 'vue';
import JsTreeList from '../js/js-tree-list.js';


//let _config = require('/../../appsettings.json');
//let BASE_URL = _config.AppSettings.BaseDomain;;
//let BASE_URLDoamin = _config.AppSettings.Domain;;

export function unflatten(arr) {

    //var tree = [],
    //    mappedArr = {},
    //    arrElem,
    //    mappedElem;

    //// First map the nodes of the array to an object -> create a hash table.
    //for (var i = 0, len = arr.length; i < len; i++) {
    //    arrElem = arr[i];
    //    mappedArr[arrElem.id] = arrElem;
    //    mappedArr[arrElem.id]['children'] = [];
    //}
    //for (var id in mappedArr) {
    //    if (mappedArr.hasOwnProperty(id)) {
    //        mappedElem = mappedArr[id];
    //        // If the element is not at the root level, add it to its parent array of children.
    //        if (mappedElem.parentId) {
    //            try {

    //                mappedArr[mappedElem['parentId']]['children'].push(mappedElem);
    //            }
    //            catch (ex) {
    //                console.log(ex);
    //            }
    //        }
    //        // If the element is at the root level, add it to first level elements array.
    //        else {
    //            tree.push(mappedElem);
    //        }
    //    }
    //}
    //console.log(tree);

    var ltt = new JsTreeList.ListToTree(arr, {
        key_id: 'id',
        key_parent: 'parentId',
        key_child: "children"
    })

    var tree = ltt.GetTree()

    return tree;
};

export function unflatten2(arr) {
    //var ltt = new JsTreeList.ListToTree(arr, {
    //    key_id: 'id',
    //    key_parent: 'parentId',
    //    key_child: "_children"
    //})

    var tree = unflatten(arr);
    return tree;
};

function unflatten(arr) {
    var tree = [],
        mappedArr = {},
        arrElem,
        mappedElem;
    // First map the nodes of the array to an object -> create a hash table.
    for (var i = 0, len = arr.length; i < len; i++) {
        arrElem = arr[i];
        mappedArr[arrElem.id] = arrElem;
        mappedArr[arrElem.id]['children'] = [];
    }
    for (var id in mappedArr) {
        if (mappedArr.hasOwnProperty(id)) {
            mappedElem = mappedArr[id];
            // If the element is not at the root level, add it to its parent array of children.
            if (mappedElem.parentId) {
                try {
                    mappedArr[mappedElem['parentId']]['children'].push(mappedElem);
                }
                catch (ex) {
                    console.log(ex);
                }
            }
            // If the element is at the root level, add it to first level elements array.
            else {
                tree.push(mappedElem);
            }
        }
    }
    return tree;
};




export function slug(title) {
    var slug = "";
    if (title != null && title != undefined && title.length > 0) {
        // Change to lower case
        var titleLower = title.toLowerCase();
        // Letter "e"
        slug = titleLower.replace(/e|é|è|ẽ|ẻ|ẹ|ê|ế|ề|ễ|ể|ệ/gi, 'e');
        // Letter "a"
        slug = slug.replace(/a|á|à|ã|ả|ạ|ă|ắ|ằ|ẵ|ẳ|ặ|â|ấ|ầ|ẫ|ẩ|ậ/gi, 'a');
        // Letter "o"
        slug = slug.replace(/o|ó|ò|õ|ỏ|ọ|ô|ố|ồ|ỗ|ổ|ộ|ơ|ớ|ờ|ỡ|ở|ợ/gi, 'o');
        // Letter "u"
        slug = slug.replace(/u|ú|ù|ũ|ủ|ụ|ư|ứ|ừ|ữ|ử|ự/gi, 'u');
        // Letter "d"
        slug = slug.replace(/đ/gi, 'd');
        // Trim the last whitespace
        slug = slug.replace(/\s*$/g, '');
        // Change whitespace to "-"
        slug = slug.replace(/\s+/g, '-');
    }
    return slug;
};



export function pathImg(title) {
    if (title != null && title != undefined && title.length > 0) {
        title = config.BASE_URLCms + "uploads/thumb" + title;
    }
    return title;

};

export function urlBase(title) {
    if (title != null && title != undefined && title.length > 0) {
        title = config.BASE_URLWeb + title + ".html";
    }
    return title;

};

