jQuery(document).ready(function($){
	function init(){
		$('.datedropper').remove();
	    $('.gfield_jb_datedropper').each(function(){
    		if($(this).attr('data-submit-value') != ""){
			var dte = new Date($(this).attr('data-submit-value'));
				var m = dte.getMonth() + 1,
					d = dte.getDate(),
					y = dte.getFullYear();
				var sd = m + '-' + d + '-' + y;
				$(this).attr('data-default-date',sd);
			}
			$(this).dateDropper();
		});
	}
	init();
	jQuery(document).on('gform_post_render', function(){
		init();
	});
});