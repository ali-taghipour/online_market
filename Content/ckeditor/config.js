/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'fa';
	 config.extraPlugins = 'uploadimage';
    //config.uploadUrl = '/uploader/upload.php';
    //config.filebrowserBrowseUrl = '/ckfinder/ckfinder.html';
    //config.filebrowserImageBrowseUrl = '/ckfinder/ckfinder.html?type=Images';
    //config.filebrowserUploadUrl = '/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Files';
	 config.filebrowserImageUploadUrl = '/Areas/Manage/Helper/CKUploader.ashx';
};
