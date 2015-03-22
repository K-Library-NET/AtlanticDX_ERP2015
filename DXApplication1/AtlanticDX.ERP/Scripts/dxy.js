// tag
// var index = 0;
// function addPanel(){
//     index++;
//     $('#tt').tabs('add',{
//         title: 'Tab'+index,
//         content: '<div style="padding:10px;width:1420">Content'+index+'</div>',
//         closable: true
//     });
// }
// function removePanel(){
//     var tab = $('#tt').tabs('getSelected');
//     if (tab){
//         var index = $('#tt').tabs('getTabIndex', tab);
//         $('#tt').tabs('close', index);
//     }
// }
$(function(){
    //search box

    var the_width = parseInt(document.body.clientWidth);
    var the_height = $(window).height();

    $(".left_scroll").css("height", the_height - 110);

    $(".right_content").css("width", the_width - 224);
    $(".right_content").css("height", the_height - 68);

	$("#search").click(function(){
		if(document.getElementById("search").value == '输入您搜索的关键字'){
			document.getElementById("search").value = ''
		}
	}).blur(function(){
		if(document.getElementById("search").value == ''){
			document.getElementById("search").value = '输入您搜索的关键字'
		}
	})
//left_nav
	// $(".left_nav .first").click(function(){
	// 	$(".left_nav .first").removeClass("click")
	// 	$(this).addClass('click')
	// 	$(".left_nav .secondary").hide()
	// 	$(this).find(".secondary").show(200)
	// }).mouseleave(function(){
	// 	window.hideTimer = setTimeout(function(){
	// 		$(this).find(".secondary").hide()
	// 	},300);
	// }).mouseover(function(){
	// 	clearTimeout(window.hideTimer);
	// });
	$(".left_nav .first").click(function(){
		$(".left_nav .first").removeClass("click")
		$(this).addClass('click')
		$(".left_nav .secondary").hide()
		$(this).find(".secondary").css("display","block")

		var the_sec_height = parseInt($(this).find(".secondary").css("height"));
		//alert(the_sec_height);
		var the_one_top = parseInt($(this).offset().top);
		var the_window_height = parseInt(document.body.clientHeight);

		if (the_sec_height >= the_window_height) {

		    $(this).find(".secondary").css("maxHeight", (the_window_height) + "px");
		    $(this).find(".secondary").css("top", -(the_one_top-68)+"px");

		}
		else {
		    if ((the_one_top - 68) > (the_sec_height / 2)) {

		        $(this).find(".secondary").css("top", -((the_sec_height-60) / 2) + "px");

		    }
		    else {

		        $(this).find(".secondary").css("top", -(the_one_top - 68) + "px");

		    }
		    

		}

		

	})
	$(".left_nav").mouseleave(function(){
		$(this).find(".secondary").hide()
	})
})
window.onload = function(){
//Computing subject width
	new function(){
		$(".header").width($(window).width()+20)
		$(".left_nav").height($(window).height()-68)
		if($(window).width()>1280){

			if($(".calculate").width() <= 1056){
				$(".right_content_tag").width($(window).width()-224)
			}
			else{
				if($(window).width()-224 > $(".calculate").width()){
					$(".right_content_tag").width($(window).width()-224)
				}
				$(".main").width($(".calculate").width()+224)
			}
		}
		else{
			if($(".calculate").width() <= 1056){
				$(".right_content_tag").width(1056)
			}
			else{
				$(".right_content_tag").width($(".calculate").width())
				$(".main").width($(".calculate").width()+224)
			}
		}
		$(".header .wrapper").css({"backgroundColor":"#35acdd"})
		$(".right_content .svs_page").width($(".right_content .calculate>table").width()-2)
		$(".svs_table_title div").width($(".right_content .calculate>table").width())

	}
}
window.onresize = function(){
//Computing subject width
	new function(){
		if($(window).width()>1280){
			if($(".calculate").width() <= 1056){
				$(".right_content_tag").width($(window).width()-224)
			}
			else{
				if($(window).width()-224 > $(".calculate").width()){
					$(".right_content_tag").width($(window).width()-224)
				}
				$(".main").width($(".calculate").width()+224)
			}
		}
		else{
			if($(".calculate").width() <= 1056){
				$(".right_content_tag").width(1056)
			}
		}
		$(".header").width($(window).width()+20)
		$(".left_nav").height($(window).height()-68)
		$(".right_content .svs_page").width($(".right_content .calculate>table").width()-2)
		$(".svs_table_title div").width($(".right_content .calculate>table").width())
	}
}
//Computing subject width and set background
$(window).scroll(function(){
	$(".header .wrapper").css({"backgroundColor":"#35acdd"})
})


