<div class="block05" uk-slideshow="animation: push">

    <div class="uk-position-relative uk-visible-toggle uk-light" tabindex="-1">

        <ul class="uk-slideshow-items" uk-height-viewport="min-height: 300">
            <?php for ($i=1;$i<=3;$i++): ?>
                <li>

                    <img style="opacity: 1" src="images/photo.jpg" alt="" uk-cover>
                    <div class="uk-height-1-1 uk-position-cover uk-background-02 uk-flex uk-flex-middle">
                        <div class="uk-width-1-1 uk-section">
                            <div class="uk-container uk-container-expand-right" uk-toggle="cls: block05__container-expand-right; mode: media; media: @m">
                                <div class="uk-flex-middle uk-grid-144-m" uk-grid>
                                    <div class="uk-width-1-2@l uk-width-auto@xl uk-flex-last@l">
                                        <img src="images/Group90.png" alt="">
                                    </div>
                                    <div class="uk-width-expand">
                                        <div class="uk-text-center">
                                            <h2 class="uk-h2 block05__title">Lorem Ipsum</h2>
                                            <p class="block05__desc">Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, remaining essentially unchanged.</p>
                                            <a href="" class="block05__btn uk-button uk-button-secondary uk-button-large uk-border-pill">ĐẶT HÀNG ONLINE</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </li>
            <?php endfor; ?>
        </ul>

        <a class="uk-position-center-left uk-position-small uk-hidden-hover" href="#" uk-slidenav-previous uk-slideshow-item="previous"></a>
        <a class="uk-position-center-right uk-position-small uk-hidden-hover" href="#" uk-slidenav-next uk-slideshow-item="next"></a>

        <ul class="uk-slideshow-nav uk-dotnav uk-flex-center uk-position-medium uk-position-bottom"></ul>
    </div>

</div>