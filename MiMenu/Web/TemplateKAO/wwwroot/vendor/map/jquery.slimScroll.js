/*! Copyright (c) 2011 Piotr Rochala (http://rocha.la)
* Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
* and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
*
* Version: 0.8.0
* editted, do not replace!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
* 8/3/2013: Edit: add trigger onScroll for scroll
*/
(function ($) {

    jQuery.fn.extend({
        slimScroll: function (options) {

            var defaults = {
                wheelStep: 20,
                width: null,
                height: null,
                size: '7px',
                color: '#000',
                position: 'right',
                distance: '2px',
                start: 'top',
                opacity: .3,
                alwaysVisible: false,
                railVisible: false,
                railColor: '#333',
                railOpacity: '0.2',
                railClass: 'slimScrollRail',
                barClass: 'slimScrollBar',
                wrapperClass: 'slimScrollDiv',
                allowPageScroll: false,
                scroll: 0,
                float: 'left'
            };

            var o = ops = $.extend(defaults, options);

            // do it for every element that matches selector
            this.each(function () {
                var $this = $(this);

                var isOverPanel, isOverBar, isDragg, queueHide, barHeight, percentScroll, lastScroll
                divS = '<div></div>',
                minBarHeight = 30,
                releaseScroll = false,
                wheelStep = parseInt(o.wheelStep),
                cwidth = o.width != null ? o.width : $this.innerWidth(),
                cheight = o.height != null ? o.height : $this.innerHeight(),
                size = o.size,
                color = o.color,
                position = o.position,
                distance = o.distance,
                start = o.start,
                opacity = o.opacity,
                alwaysVisible = o.alwaysVisible,
                railVisible = o.railVisible,
                railColor = o.railColor,
                railOpacity = o.railOpacity,
                allowPageScroll = o.allowPageScroll,
                scroll = o.scroll,
                float = o.float;
                // used in event handlers and for better minification
                var me = $(this);

                //ensure we are not binding it again
                if (me.parent().hasClass('slimScrollDiv')) {
                    //check if we should scroll existing instance
                    if (scroll) {
                        //find bar and rail
                        bar = me.parent().find('.slimScrollBar');
                        rail = me.parent().find('.slimScrollRail');
                        scrollContent(me.scrollTop() + parseInt(scroll), false, true);
                    }
                    return;
                }
                $this.on('updateScroll', function () {
                    getBarHeight();
                    showBar();
                    //scroll by given amount of pixels                                                
                    if (start == 'bottom') {
                        bar.css({ top: me.outerHeight() - bar.outerHeight() });
                        scrollContent(0, true);
                    } else if (start == 'top') {
                        bar.css({ top: 0 });
                        scrollContent(0, null, true);
                    }
                    else if (typeof start == 'object') {
                        // scroll content
                        scrollContent($(start).position().top, null, true);

                        // make sure bar stays hidden
                        if (!alwaysVisible) {
                            bar.hide();
                        }
                    }                    
                });
                // wrap content
                var wrapper = $(divS).addClass(o.wrapperClass)
                      .css({
                          position: 'relative',
                          overflow: 'hidden',
                          width: cwidth
                      });
                if (cheight > 0) wrapper.css({ height: cheight });
                // update style for the div
                me.css({
                    overflow: 'hidden',
                    width: cwidth,
                    height: cheight
                });

                // create scrollbar rail
                var rail = $(divS)
          .addClass(o.railClass)
          .css({
              width: size,
              height: '100%',
              position: 'absolute',
              top: 0,
              display: (alwaysVisible && railVisible) ? 'block' : 'none',
              'border-radius': size,
              background: railColor,
              opacity: railOpacity,
              zIndex: 90
          });

                // create scrollbar
                var bar = $(divS)
          .addClass(o.barClass)
          .css({
              background: color,
              width: size,
              position: 'absolute',
              top: 0,
              opacity: opacity,
              display: alwaysVisible ? 'block' : 'none',
              'border-radius': size,
              BorderRadius: size,
              MozBorderRadius: size,
              WebkitBorderRadius: size,
              zIndex: 99
          });

                // set position
                var posCss = (position == 'right') ? { right: distance} : { left: distance };
                rail.css(posCss);
                bar.css(posCss);

                // wrap it
                me.wrap(wrapper);

                // append to parent div
                me.parent().append(bar);
                me.parent().append(rail);

                // make it draggable
                bar.draggable({
                    axis: 'y',
                    containment: 'parent',
                    start: function () { isDragg = true; },
                    stop: function () {
                        isDragg = false;
                        //hideBar();
                        unZoomBar(); },
                    drag: function (e) {
                        // scroll content
                        zoomBar();
                        scrollContent(0, $(this).position().top, false);
                    }
                });

                // on rail over
                rail.hover(function () {
                    showBar();
                }, function () {
                    //hideBar();
                });

                // on bar over
                bar.hover(function () {
                    isOverBar = true;
                    zoomBar();
                }, function () {
                    isOverBar = false;
                    unZoomBar();
                });

                // show on parent mouseover
                me.hover(function () {
                    isOverPanel = true;
                    showBar();
                    //hideBar();
                }, function () {
                    isOverPanel = false;
                    hideBar();
                });

                var _onWheel = function (e) {
                    // use mouse wheel only when mouse is over
                    if (!isOverPanel) { return; }

                    var e = e || window.event;

                    var delta = 0;
                    if (e.wheelDelta) { delta = -e.wheelDelta / 120; }
                    if (e.detail) { delta = e.detail / 3; }

                    // scroll content
                    scrollContent(delta, true);

                    // stop window scroll
                    if (e.preventDefault && !releaseScroll) { e.preventDefault(); }
                    if (!releaseScroll) { e.returnValue = false; }
                }

                function scrollContent(y, isWheel, isJump) {
                    var delta = y;

                    if (isWheel) {
                        // move bar with mouse wheel
                        delta = parseInt(bar.css('top')) + y * wheelStep / 100 * bar.outerHeight();

                        // move bar, make sure it doesn't go out
                        var maxTop = me.outerHeight() - bar.outerHeight();
                        delta = Math.min(Math.max(delta, 0), maxTop);

                        // scroll the scrollbar
                        bar.css({ top: delta + 'px' });
                        me.trigger('onScroll');
                    }

                    // calculate actual scroll amount
                    percentScroll = parseInt(bar.css('top')) / (me.outerHeight() - bar.outerHeight());
                    delta = percentScroll * (me[0].scrollHeight - me.outerHeight());

                    if (isJump) {
                        delta = y;
                        var offsetTop = delta / me[0].scrollHeight * me.outerHeight();
                        bar.css({ top: offsetTop + 'px' });
                        me.trigger('onScroll');
                    }

                    // scroll content
                    me.scrollTop(delta);

                    // ensure bar is visible
                    showBar();

                    // trigger hide when scroll is stopped
                   
                   // hideBar();
                    $('#ui-datepicker-div').hide();
                }

                var attachWheel = function () {
                    if (window.addEventListener) {
                        this.addEventListener('DOMMouseScroll', _onWheel, false);
                        this.addEventListener('mousewheel', _onWheel, false);
                    }
                    else {
                        document.attachEvent("onmousewheel", _onWheel)
                    }
                }

                // attach scroll events
                attachWheel();

                function getBarHeight() {
                    // calculate scrollbar height and make sure it is not too small
                    barHeight = Math.max((me.outerHeight() / me[0].scrollHeight) * me.outerHeight(), minBarHeight);
                    bar.css({ height: barHeight + 'px' });
                }

                // set up initial height
                getBarHeight();

                function showBar() {
                    // recalculate bar height
                    getBarHeight();
                    clearTimeout(queueHide);

                    // release wheel when bar reached top or bottom
                    releaseScroll = allowPageScroll && percentScroll == ~ ~percentScroll;

                    // show only when required
                    if (barHeight >= me.outerHeight()) {
                        //allow window scroll
                        releaseScroll = true;
                        return;
                    }
                    // when bar reached top or bottom
                    if (percentScroll == ~ ~percentScroll) {
                        //release wheel 
                        releaseScroll = o.allowPageScroll;

                        // publish approporiate event
                        if (lastScroll != percentScroll) {
                            var msg = (~ ~percentScroll == 0) ? 'top' : 'bottom';
                            me.trigger('slimscroll', msg);
                        }
                    }
                    lastScroll = percentScroll;

                    bar.stop(true, true).fadeIn('fast');
                    if (railVisible) { rail.stop(true, true).fadeIn('fast'); }
                }

                function zoomBar() {
                    bar.css({ width: rail.width() + 3 });
                }

                function unZoomBar() {
                    bar.css({ width: rail.width()});
                }
                function hideBar() {
                    // only hide when options allow it
                    if (!alwaysVisible) {
                        queueHide = setTimeout(function () {
                            if (!isOverBar && !isDragg) {
                                bar.fadeOut('slow');
                                rail.fadeOut('slow');
                            }
                        }, 0);
                    }
                }

                // check start position
                if (start == 'bottom') {
                    // scroll content to bottom
                    bar.css({ top: me.outerHeight() - bar.outerHeight() });
                    scrollContent(0, true);
                }
                else if (typeof start == 'object') {
                    // scroll content
                    bar.css({ top: me.outerHeight() - bar.outerHeight() });
                    scrollContent($(start).position().top, null, true);

                    // make sure bar stays hidden
                    if (!alwaysVisible) { bar.hide(); }
                }
            });

            // maintain chainability
            return this;
        }
    });

    jQuery.fn.extend({
        slimscroll: jQuery.fn.slimScroll
    });

})(jQuery);