/* JS Document */

/******************************

[Table of Contents]

1. Vars and Inits
2. Init Menu
3. Init Isotope


******************************/

$(document).ready(function()
{
	"use strict";

	/* 

	1. Vars and Inits

	*/

	var menu = $('.menu');
	var burger = $('.hamburger');
	var menuActive = false;

	$(window).on('resize', function()
	{
		setTimeout(function()
		{
			$(window).trigger('resize.px.parallax');
		}, 375);
	});

	initMenu();
	initHomeSlider();
	initIsotope();


	/* 

	2. Init Menu

	*/

	function initMenu()
	{
		if(menu.length)
		{
			if($('.hamburger').length)
			{
				burger.on('click', function()
				{
					if(menuActive)
					{
						closeMenu();
					}
					else
					{
						openMenu();

						$(document).one('click', function cls(e)
						{
							if($(e.target).hasClass('menu_mm'))
							{
								$(document).one('click', cls);
							}
							else
							{
								closeMenu();
							}
						});
					}
				});
			}
		}
	}

	function openMenu()
	{
		menu.addClass('active');
		menuActive = true;
	}

	function closeMenu()
	{
		menu.removeClass('active');
		menuActive = false;
	}

	/* 

	3. Init Home SLider

	*/

	function initHomeSlider()
	{
		if($('.home_slider').length)
		{
			var homeSlider = $('.home_slider');
			homeSlider.owlCarousel(
			{
				items:1,
				loop:true,
				autoplay:false,
				smartSpeed:1200
			});

			if($('.home_slider_prev').length)
			{
				var prev = $('.home_slider_prev');
				prev.on('click', function()
				{
					homeSlider.trigger('prev.owl.carousel');
				});
			}

			if($('.home_slider_next').length)
			{
				var next = $('.home_slider_next');
				next.on('click', function()
				{
					homeSlider.trigger('next.owl.carousel');
				});
			}

		}
	}

	/* 

	3. Init Isotope Filtering

	*/

    function initIsotope()
    {
    	var sortingButtons = $('.item_sorting_btn');

    	if($('.grid').length)
    	{
    		var grid = $('.grid').isotope({
	  			itemSelector: '.grid-item',
	  			percentPosition: true,
	  			masonry:
	  			{
				    horizontalOrder: true
			  	},
			  	getSortData:
	            {
	            	price: function(itemElement)
	            	{
	            		var priceEle = $(itemElement).find('.product_price').text().replace( 'PLN', '' );
	            		return parseFloat(priceEle);
	            	},
	            	name: '.product_title'
	            }
	        });

	        sortingButtons.each(function()
	        {
	        	$(this).on('click', function()
	        	{
	        		var parent = $(this).parent().parent().find('.isotope_sorting_text span');
		        		parent.text($(this).text());
		        		var option = $(this).attr('data-isotope-option');
		        		option = JSON.parse( option );
	    				grid.isotope( option );
	        	});
	        });

	        // Filtering
	        $('.item_filter_btn').on('click', function()
	        {
	        	var parent = $(this).parent().parent().find('.isotope_filter_text span');
	        	parent.text($(this).text());
		        var filterValue = $(this).attr('data-filter');
				grid.isotope({ filter: filterValue });
	        });
    	}
    }

});