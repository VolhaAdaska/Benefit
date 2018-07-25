/*
Theme Name: Weny
Description: Coming Soon Template
Author: Erilisdesign
Theme URI: http://erilisdesign.com/preview/themeforest/html/weny/
Author URI: http://themeforest.net/user/Erilisdesign
Version: 1.5
License: https://themeforest.net/licenses/standard
*/

(function($) {
	"use strict";
	
	// Var
	var body = $('body');

	// BOOTSTRAP FIX FOR WINPHONE 8 AND IE10
	if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
		var msViewportStyle = document.createElement('style');
		msViewportStyle.appendChild(
			document.createTextNode(
				'@-ms-viewport{width:auto!important}'
			)
		);
		document.getElementsByTagName("head")[0].appendChild(msViewportStyle);
	}

	if( /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) ) {
		$('body').addClass('mobile');
	}

	function detectIE() {
		if (navigator.userAgent.indexOf('MSIE') != -1)
			var detectIEregexp = /MSIE (\d+\.\d+);/ // test for MSIE x.x
		else // if no "MSIE" string in userAgent
			var detectIEregexp = /Trident.*rv[ :]*(\d+\.\d+)/ // test for rv:x.x or rv x.x where Trident string exists

		if (detectIEregexp.test(navigator.userAgent)){ // if some form of IE
			var ieversion=new Number(RegExp.$1) // capture x.x portion and store as a number
			if (ieversion >= 9) {
				return true;
			}
		}
		return false;
	}

	function getWindowWidth() {
		return Math.max( $(window).width(), window.innerWidth);
	}

	function getWindowHeight() {
		return Math.max( $(window).height(), window.innerHeight);
	}
	
	function isTouchSupported() {
		var msTouchEnabled = window.navigator.msMaxTouchPoints;
		var generalTouchEnabled = "ontouchstart" in document;
		if (msTouchEnabled || generalTouchEnabled) {
			return true;
		}
		return false;
	}
	
	jQuery.fn.setAllToMaxHeight = function(){
		return this.css({ 'height' : '' }).height( Math.max.apply(this, jQuery.map( this , function(e){ return jQuery(e).height() }) ) );
	};
	

	// Preloader
	function initPreloader() {
		var preloaderDelay = 350;
		var	preloaderFadeOutTime = 800;

		function hidePreloader() {
			var preloader = $('#preloader');
			
			preloader.delay(preloaderDelay).fadeOut(preloaderFadeOutTime);
		}

		hidePreloader();
	}


	// Animations
	function initAnimations() {
		var windowWidth = getWindowWidth();
			
		if(windowWidth <=1199 || $('body').hasClass('mobile')) {
			$('.animated').css({
				'display':'block',
				'visibility':'visible'
			});
			$('.button.animated').css({
				'display':'inline-block'
			});
		} else {
			if( detectIE() ) {
				$('.animated').css({
					'display':'block',
					'visibility':'visible'
				});
				$('.button.animated').css({
					'display':'inline-block'
				});
			} else {
				
				// Staticpage animations
				if(body.hasClass('staticpage')){
					
					$('.animated').appear();
					
					$('.animated').on('appear', function(event, $all_appeared_elements) {
						var elem = $(this);
						var animation = elem.data('animation');
						
						if(elem.parents('.swiper-slider').length>0 || elem.parents('.flexslider').length>0){
							return true;
						}
						
						if ( !elem.hasClass('visible') ) {
							var animationDelay = elem.data('animation-delay');
							if ( animationDelay ) {
								setTimeout(function(){
									elem.addClass( animation + ' visible' );
								}, animationDelay);
							} else {
								elem.addClass( animation + ' visible' );
							}
						}
					});
					
				}
				
				/* Starting Animation on Load */
				$(window).load(function() {
					$('.onstart').each( function() {
						var elem = $(this);
						if ( !elem.hasClass('visible') ) {
							var animationDelay = elem.data('animation-delay');
							var animation = elem.data('animation');
							if ( animationDelay ) {
								setTimeout(function(){
									elem.addClass( animation + " visible" );
								}, animationDelay);
							} else {
								elem.addClass( animation + " visible" );
							}
						}
					});
				});
			}		
		}
	}


	// Fullscreen Elements
	function initFullscreenElements() {
		$('.equal-section').each(function(){
			$(this).find('.equal-col').setAllToMaxHeight();
		});
	}
	
	
	// Navigation
	function initFullpage() {
		if(!body.hasClass('staticpage')){
		
			$('#bt-fullpage').fullpage({
				
				//Navigation
				menu: '#fp-menu',
				anchors: ['home', 'about', 'services', 'subscribe', 'portfolio', 'team', 'contact'],	
				slidesNavigation: true,
				slidesNavPosition: 'bottom',

				//Scrolling
				css3: true,
				scrollingSpeed: 800,
				autoScrolling: true,
				fitToSection: true,
				fitToSectionDelay: 1000,
				scrollBar: false,
				easing: 'easeInQuart',
				continuousVertical: false,
				scrollOverflow: true,
				
				//Accessibility
				keyboardScrolling: true,
				animateAnchor: false,
				recordHistory: true,
				
				//Design
				resize: false,
				responsive: 0,
				paddingTop: '0px',
				paddingBottom: '0px',
			
				//Custom selectors
				sectionSelector: '.bt-section',
				slideSelector: '.bt-slide',
				
				afterLoad: function(anchorLink, index){
					var fpActiveSection = $('.fp-section[data-anchor="'+ anchorLink +'"]');
					
					if(anchorLink == 'home'){
						body.removeClass('fp-section-active');
					} else {
						body.addClass('fp-section-active');
					}
					
					if (fpActiveSection.find('.fp-slide')){
						fpActiveSection.find('.fp-slide').first().addClass('first-slide');
					}
					
					if(!$('.fp-section[data-anchor="'+ anchorLink +'"] .fp-slide').length){
						// Animated all elements that have the ".animated" class in the Section
						if (fpActiveSection.hasClass('active')) {
							if(fpActiveSection.find('.animated')){
								fpActiveSection.find('.animated').each( function() {
									var elem = $(this);
									var animation = elem.data('animation');
									if ( !elem.hasClass('visible') ) {
										var animationDelay = elem.data('animation-delay');
										if ( animationDelay ) {
											setTimeout(function(){
												elem.addClass( animation + ' visible' );
											}, animationDelay);
										} else {
											elem.addClass( animation + ' visible' );
										}
									}
								});
							}
						}
					} else if($('.fp-section[data-anchor="'+ anchorLink +'"] .first-slide').length){
						// Animated all elements that have the ".animated" class in the first slide in the Section
						var fpFirstSlide = fpActiveSection.find('.fp-slide.first-slide');		
						if(fpFirstSlide.find('.animated')){
							fpFirstSlide.find('.animated').each( function() {
								var elem = $(this);
								var animation = elem.data('animation');
								if ( !elem.hasClass('visible') ) {
									var animationDelay = elem.data('animation-delay');
									if ( animationDelay ) {
										setTimeout(function(){
											elem.addClass( animation + ' visible' );
										}, animationDelay);
									} else {
										elem.addClass( animation + ' visible' );
									}
								}
							});
						}		
					}
				},
				afterSlideLoad: function(anchorLink, index, slideAnchor, slideIndex){
					// Animated all elements that have the ".animated" class in the nexts slides in the Section
					var fpActiveSlide = $('.fp-slide.active');		
					if(fpActiveSlide.find('.animated')){
						fpActiveSlide.find('.animated').each( function() {
							var elem = $(this);
							var animation = elem.data('animation');
							if ( !elem.hasClass('visible') ) {
								var animationDelay = elem.data('animation-delay');
								if ( animationDelay ) {
									setTimeout(function(){
										elem.addClass( animation + ' visible' );
									}, animationDelay);
								} else {
									elem.addClass( animation + ' visible' );
								}
							}
						});
					}
				}
			});
			
			$('a.moveto').on( 'click', function(event) {
				event.preventDefault();
				
				var elem = $(this),
					anchor = elem.attr('href').slice(1);
					
				$.fn.fullpage.moveTo(anchor);
			});
			
		}
	}
	
	
	// Page Layout
	function initPageLayout() {
		var windowWidth = getWindowWidth();
		
		$('a.moveto').off('click');
		$('.navbar a').off('click');
		
		if(!windowWidth <=1199 & !$('body').hasClass('mobile') & $('body').hasClass('staticpage') ) {
			
			$(window).scroll(function(){
				var scroll = $(this).scrollTop();
					
				if (scroll > 180) {
					body.addClass('fp-section-active')
				} else {
					body.removeClass('fp-section-active');
				}
			});
			
			$('a.moveto').on( 'click', function(event) {
				event.preventDefault();
				var anchor = $(this).attr('href');
				
				$.scrollTo(anchor, 800);	
			});

		} else if(windowWidth <=1199 || $('body').hasClass('mobile')) {

			if($('#bt-fullpage').hasClass('fullpage-wrapper')){
				// fullPage.js - Destroy
				$.fn.fullpage.destroy('all');
			}

			if($('#bt-fullpage').hasClass('fp-destroyed')){
				$('a.moveto').on( 'click', function(event) {
					event.preventDefault();
					var anchor = $(this).attr('href').slice(1);
					
					$.scrollTo('div[data-anchor="'+ anchor +'"]', 800, {
						offset: {top:-70}					 
					});	
				});
			} else {
				$('a.moveto').on( 'click', function(event) {
					event.preventDefault();
					var anchor = $(this).attr('href').slice(1);
					
					$.scrollTo('div[data-element-id="'+ anchor +'"]', 800, {
						offset: {top:-70}					 
					});	
				});
			}

			$('.navbar a').on('click', function(event) {
				if(!$('.navbar-toggle').hasClass('collapsed')){
					$('.navbar-toggle').trigger('click');
				}
			});
		} else {
			if($('#bt-fullpage').hasClass('fp-destroyed')){
				
				$('#bt-fullpage').find('.animated').each( function() {
					var elem = $(this);
					var animation = elem.data('animation');
					if ( !elem.hasClass('visible') ) {
						var animationDelay = elem.data('animation-delay');
						if ( animationDelay ) {
							setTimeout(function(){
								elem.addClass( animation + ' visible' );
							}, animationDelay);
						} else {
							elem.addClass( animation + ' visible' );
						}
					}
				});
				
				// fullPage.js - Rebulid
				initFullpage();
			} else if(!$('#bt-fullpage').hasClass('fullpage-wrapper')){
				// fullPage.js - Load on first time
				initFullpage();
			}
		}
		
		$('ul.dropdown-menu [data-toggle=dropdown]').on('click', function(event) {
			event.preventDefault(); 
			event.stopPropagation(); 
			$(this).parent().siblings().removeClass('open');
			$(this).parent().toggleClass('open');
		});
		
	}
	
	
	function initPageBackground() {

		// Slideshow Background
		if($('body').hasClass('slideshow-background')) {
			$('.slideshow-background').vegas({
				preload: true,
				timer: false,
				delay: 5000,
				transition: 'fade',
				transitionDuration: 1000,
				slides: [
					{ src: 'demo/images/image-21.jpg' },
					{ src: 'demo/images/image-23.jpg' },
					{ src: 'demo/images/image-13.jpg' },
					{ src: 'demo/images/image-14.jpg' },
					{ src: 'demo/images/image-29.jpg' }
				]
			});
		}

		// Slideshow & Video Background
		if($('body').hasClass('slideshow-video-background')) {
			$('.slideshow-video-background').vegas({
				preload: true,
				timer: false,
				delay: 5000,
				transition: 'fade',
				transitionDuration: 1000,
				slides: [
					{ src: 'demo/images/image-21.jpg' },
					{ src: 'demo/video/marine.jpg',
						video: {
							src: [
								'demo/video/marine.mp4',
								'demo/video/marine.webm',
								'demo/video/marine.ogv'
							],
							loop: false,
							mute: true
						}
					},
					{ src: 'demo/images/image-23.jpg' },
					{ src: 'demo/images/image-13.jpg' },
					{ src: 'demo/images/image-14.jpg' },
					{ src: 'demo/images/image-29.jpg' }
				]
			});
		}

		// Kenburns Background
		if($('body').hasClass('kenburns-background')) {
			var kenburnsDisplayBackdrops = false;
			var kenburnsBackgrounds = [
				{ src: 'demo/images/image-21.jpg', valign: 'top' },
				{ src: 'demo/images/image-23.jpg', valign: 'top' },
				{ src: 'demo/images/image-13.jpg', valign: 'top' },
				{ src: 'demo/images/image-14.jpg', valign: 'top' },
				{ src: 'demo/images/image-29.jpg', valign: 'top' }
			];

			$('.kenburns-background').vegas({
				preload: true,
				transition: 'swirlLeft2',
				transitionDuration: 4000,
				timer: false,
				delay: 10000,
				slides: kenburnsBackgrounds,
				walk: function (nb) {
					if (kenburnsDisplayBackdrops === true) {
						var backdrop;

						backdrop = backdrops[nb];
						backdrop.animation  = 'kenburns';
						backdrop.animationDuration = 20000;
						backdrop.transition = 'fade';
						backdrop.transitionDuration = 1000;

						$('body')
							.vegas('options', 'slides', [ backdrop ])
							.vegas('next');
					}
				}
			});
		}

		// Youtube Video Background
		if($('body').hasClass('youtube-background')) {
			$('.player').each(function() {
				$('.player').mb_YTPlayer();
			});
		}
		
		// Youtube Multiple Video Background
		if($('body').hasClass('youtube-multiple-background')) {
			
			var videos = [
				{videoURL: "0pXYp72dwl0", showControls:false, containment:'.overlay-video',autoPlay:true, mute:true, startAt:0,opacity:1, loop:false, ratio:"4/3", showYTLogo:false, realfullscreen: true, addRaster:true},
				{videoURL: "9d8wWcJLnFI", showControls:false, containment:'.overlay-video',autoPlay:true, mute:true, startAt:20,opacity:1, loop:false, ratio:"4/3", showYTLogo:false, realfullscreen: true, addRaster:false},
				{videoURL: "nam90gorcPs", showControls:false, containment:'.overlay-video',autoPlay:true, mute:true, startAt:20,opacity:1, loop:false, ratio:"4/3", showYTLogo:false, realfullscreen: true, addRaster:true}
			];

			$('.player').YTPlaylist(videos, true);
			
		}
		
		// Google Map Background
		gmapBackground();
		
	}
	
	function gmapBackground() {
		if($('#gmap-background').length){
			$('#gmap-background').gMap({
				controls: {
					panControl: false,
					zoomControl: false,
					mapTypeControl: false,
					scaleControl: false,
					streetViewControl: false,
					overviewMapControl: false
				},
				scrollwheel: false,
				markers:[
					{
						latitude: 37.752797,
						longitude: -122.409132,
						html: "San Francisco, USA",
						popup: false
					}
				],
				zoom: 15
			});
		}
		if($('#gmap-contact').length){
			$('#gmap-contact').gMap({
				controls: {
					panControl: false,
					zoomControl: false,
					mapTypeControl: false,
					scaleControl: false,
					streetViewControl: false,
					overviewMapControl: false
				},
				scrollwheel: false,
				markers:[
					{
						latitude: 37.752797,
						longitude: -122.409132,
						html: "San Francisco, USA",
						popup: false
					}
				],
				zoom: 15
			});
		}
	}
	

	// Plugins
	function initPlugins() {
		
		$('#gmap1').gMap({
			scrollwheel: true,
			address: "San Francisco, USA",
			zoom: 5,
			markers:[
				{
					latitude: 37.752797,
					longitude: -122.409132,
					html: "San Francisco, USA",
					popup: true
				}
			]
		});
		
		$('#gmap2').gMap({
			address: "San Francisco, USA",
			zoom: 3,
			markers:[
				{
					address: "San Francisco, USA",
					html: "San Francisco, USA"
				},
				{
					address: "New York, USA",
					html: "New York, USA"
				},
				{
					address: "Miami, USA",
					html: "Miami, USA"
				}
			]
		});
		
		// Responsive Video - FitVids
		$('.video-container').fitVids();
		
		// Sliders
		$('.text-slider').flexslider({
			animation: 'fade',
			animationLoop: true,
			slideshowSpeed: 7000,
			animationSpeed: 700,
			controlNav: true,
			directionNav: false,
			keyboard: false,
			smoothHeight: false
		});
	
		// Countdown
		if ($('.countdown[data-countdown]').length) {			
			$('.countdown[data-countdown]').each(function() {
				var $this = $(this),
					finalDate = $(this).data('countdown');
				$this.countdown(finalDate, function(event) {
					$this.html(event.strftime(
						'<div class="counter-container"><div class="counter-box first"><div class="number">%-D</div><span>Day%!d</span></div><div class="counter-box"><div class="number">%H</div><span>Hours</span></div><div class="counter-box"><div class="number">%M</div><span>Minutes</span></div><div class="counter-box last"><div class="number">%S</div><span>Seconds</span></div></div>'
					));
				});
			});
		}

		// Placeholder
		$('input, textarea').placeholder();
		
		// Tooltip
		$('[data-toggle="tooltip"]').tooltip();
		
		// Popover
		$('[data-toggle="popover"]').popover();
	
	}
	
	
	// magnificPopup
	function initMagnificPopup() {
		$('.mfp-image').magnificPopup({
			type:'image',
			closeMarkup: '<button title="%title%" type="button" class="mfp-close"><i class="ion-android-close"></i></button>',
			removalDelay: 300,
			mainClass: 'mfp-fade'
		});
		
		$('.mfp-gallery').each(function() {
			$(this).magnificPopup({
				delegate: 'a',
				type: 'image',
				gallery: {
					enabled: true
				},
				closeMarkup: '<button title="%title%" type="button" class="mfp-close"><i class="ion-android-close"></i></button>',
				removalDelay: 300,
				mainClass: 'mfp-fade'
			});
		});
		
		$('.mfp-iframe').magnificPopup({
			type: 'iframe',
			iframe: {
				patterns: {
					youtube: {
						index: 'youtube.com/',
						id: 'v=',
						src: '//www.youtube.com/embed/%id%?autoplay=1' // URL that will be set as a source for iframe.
					},
					vimeo: {
						index: 'vimeo.com/',
						id: '/',
						src: '//player.vimeo.com/video/%id%?autoplay=1'
					},
					gmaps: {
						index: '//maps.google.',
						src: '%id%&output=embed'
					}
				},
				srcAction: 'iframe_src'
			},
			closeMarkup: '<button title="%title%" type="button" class="mfp-close"><i class="ion-android-close"></i></button>',
			removalDelay: 300,
			mainClass: 'mfp-fade'
		});
		
		$('.mfp-ajax').magnificPopup({
			type: 'ajax',
			ajax: {
				settings: null,
				cursor: 'mfp-ajax-cur',
				tError: '<a href="%url%">The content</a> could not be loaded.'
			},
			midClick: true,
			closeMarkup: '<button title="%title%" type="button" class="mfp-close"><i class="ion-android-close"></i></button>',
			removalDelay: 300,
			mainClass: 'mfp-fade',
			callbacks: {
				ajaxContentAdded: function(mfpResponse) {
					initFlexslider();
				}
			}
		});
		
		$('.open-popup-link').magnificPopup({
			type: 'inline',
			midClick: true,
			closeMarkup: '<button title="%title%" type="button" class="mfp-close"><i class="ion-android-close"></i></button>',
			removalDelay: 300,
			mainClass: 'mfp-fade'
		});
	}
	

	// Mailchimp
	function initMailchimp() {
		$('.mailchimp-form').ajaxChimp({
			callback: mailchimpCallback,
			url: "mailchimp-post-url" //Replace this with your own mailchimp post URL. Don't remove the "". Just paste the url inside "".  
		});

		function mailchimpCallback(resp) {
			 if (resp.result === 'success') {
				$('.success-message').html(resp.msg).fadeIn(1000);
				$('.error-message').fadeOut(500);		
			} else if(resp.result === 'error') {
				$('.error-message').html(resp.msg).fadeIn(1000);
			}  
		}

		$('#email').focus(function(){
			$('.error-message').fadeOut();
			$('.success-message').fadeOut();
		});

		$('#email').on('keydown', function(){
			$('.error-message').fadeOut();
			$('.success-message').fadeOut();
		});

		$("#email").on('click', function() {
			$("#email").val('');
		});
	}
	

	// Contact Form
	function initContactForm() {
		var $contactForm = $('.contact-form');
		if( $contactForm.length < 1 ){ return true; }

		$contactForm.each( function(){
			var element = $(this),
				elementAlert = element.attr('data-alert-type'),
				elementResult = element.find('.contact-form-result');

			element.find('form').validate({
				submitHandler: function(form) {
					elementResult.hide();

					$(form).ajaxSubmit({
						target: elementResult,
						dataType: 'json',
						success: function( data ) {
							elementResult.html( data.message ).fadeIn( 400 );
							if( data.alert != 'error' ) { $(form).clearForm(); }
						}
					});
				}
			});

		});
	}
	
	// WINDOW.LOAD FUNCTION
	$(window).load(function() {		
		initPreloader();
	});
	
	// DOCUMENT.READY FUNCTION
	jQuery(document).ready(function($) {
		initFullscreenElements();
		initAnimations();	
		initPageBackground();
		initPageLayout();
		initPlugins();
		initMagnificPopup();
		initMailchimp();
		initContactForm();
	});
	
	// WINDOW.RESIZE FUNCTION
	$(window).on('resize', function () {
		initAnimations();
		initFullscreenElements();
		initPageLayout();
		gmapBackground();
	});

})(jQuery);

//Google Tracking Code
(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

ga('create', 'UA-85701226-1', 'auto');
ga('send', 'pageview');