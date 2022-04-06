<?php $data["title"] = "Dat hang"; ?>
<?php $is_home = false; ?>
<?php $ratio = '0.9'; ?>
<?php require "template-parts/layouts/header.php"; ?>
<div class="uk-background-norepeat menu__section1 uk-position-relative uk-background-cover uk-light" data-src="images/05600a6dc19a37ef746fb267902398bf2.png" uk-img>
    <div class="uk-position-cover menu__overlay"></div>
    <div class="uk-position-relative uk-position-z-index uk-section-small uk-text-center">
        <div class="uk-container">
            <h1 class="uk-h1 menu__title2">Menu</h1>
            <ul class="uk-breadcrumb">
                <li><a href="">Home</a></li>
                <li><a href="">Menu</a></li>
                <li><span>Đặt hàng</span></li>
            </ul>
        </div>
    </div>
</div>
<div class="uk-section-small uk-background-muted" uk-height-viewport="offset-bottom: true;offset-top: true">
    <div class="uk-container">
        <div class="uk-grid-small uk-grid-20-m" uk-grid>
            <div class="uk-width-expand">
                <div class="uk-card uk-card-default uk-card-body order__boxForm__card1">
                    <form>
                        <fieldset class="uk-fieldset">
                            <legend class="uk-legend order__boxForm__title1">THÔNG TIN THANH TOÁN</legend>
                            <div class="uk-margin">
                                <div class="uk-margin-small">
                                    <label><input class="uk-radio" type="radio" name="radio2" checked> <span class="order__boxForm__label2">Ngồi tại quán</span></label>
                                </div>
                                <div class="uk-margin-small">
                                    <label class="uk-form-label order__boxForm__label2" for="form-stacked-text">Số bàn</label>
                                    <div class="uk-form-controls">
                                        <div class="uk-inline uk-width-1-1">
                                            <span class="uk-form-icon uk-form-icon-flip order__boxForm__icon1" uk-icon="icon: location; ratio: <?= $ratio ?>"></span>
                                            <input class="uk-input uk-border-pill" type="text">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="uk-margin">
                                <div class="uk-margin-small">
                                    <label><input class="uk-radio" type="radio" name="radio2"> <span class="order__boxForm__label2">Giao về</span></label>
                                </div>
                                <div class="uk-card order__boxForm__card2 uk-card-body uk-card-default uk-margin-small">
                                    <div>
                                        <fieldset class="uk-fieldset">

                                            <legend class="uk-legend uk-text-center uk-text-uppercase order__boxForm__title2">Thông tin khách hàng</legend>

                                            <div class="uk-margin">
                                                <div class="uk-grid-small uk-grid-20-m" uk-grid>
                                                    <div class="uk-width-1-1">
                                                        <div class="uk-form-stacked">
                                                            <label class="uk-form-label order__boxForm__label2" for="form-stacked-text">Địa chỉ giao hàng</label>
                                                            <div class="uk-form-controls">
                                                                <div class="uk-inline uk-width-1-1">
                                                                    <span class="uk-form-icon uk-form-icon-flip" uk-icon="icon: location; ratio: <?= $ratio ?>"></span>
                                                                    <input class="uk-input uk-border-pill" type="text">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="uk-width-1-1">
                                                        <div class="uk-form-stacked">
                                                            <label class="uk-form-label order__boxForm__label2" for="form-stacked-text">Người nhận</label>
                                                            <div class="uk-form-controls">
                                                                <div class="uk-inline uk-width-1-1">
                                                                    <span class="uk-form-icon uk-form-icon-flip" uk-icon="icon: user; ratio: <?= $ratio ?>"></span>
                                                                    <input class="uk-input uk-border-pill" type="text">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="uk-width-1-2@s">
                                                        <div class="uk-form-stacked">
                                                            <label class="uk-form-label order__boxForm__label2" for="form-stacked-text">Điện thoại</label>
                                                            <div class="uk-form-controls">
                                                                <div class="uk-inline uk-width-1-1">
                                                                    <span class="uk-form-icon uk-form-icon-flip" uk-icon="icon: receiver; ratio: <?= $ratio ?>"></span>
                                                                    <input class="uk-input uk-border-pill" type="tel">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="uk-width-1-2@s">
                                                        <div class="uk-form-stacked">
                                                            <label class="uk-form-label order__boxForm__label2" for="form-stacked-text">Email (không bắt buộc)</label>
                                                            <div class="uk-form-controls">
                                                                <div class="uk-inline uk-width-1-1">
                                                                    <span class="uk-form-icon uk-form-icon-flip" uk-icon="icon: mail; ratio: <?= $ratio ?>"></span>
                                                                    <input class="uk-input uk-border-pill" type="email">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="uk-width-1-1">
                                                        <div class="uk-form-stacked">
                                                            <label class="uk-form-label order__boxForm__label2" for="form-stacked-text">Ghi chú (không bắt buộc)</label>
                                                            <div class="uk-form-controls">
                                                                <div class="uk-inline uk-width-1-1">
                                                                    <span class="uk-form-icon uk-form-icon-flip" uk-icon="icon: pencil; ratio: <?= $ratio ?>"></span>
                                                                    <textarea class="uk-textarea" rows="5" placeholder=""></textarea>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="uk-margin uk-form-stacked">
                                                <div class="uk-form-label order__boxForm__label2 order__boxForm__label2--c1">Hình thức thanh toán</div>
                                                <div class="uk-form-controls">
                                                    <label><input class="uk-radio" type="radio" name="radio1"> <span class="order__boxForm__label2">Thanh toán khi nhận hàng (COD)</span></label>
                                                </div>
                                            </div>
                                            <div class="uk-margin uk-form-stacked">
                                                <div class="uk-form-label order__boxForm__label2 order__boxForm__label2--c1">Chọn thời gian giao hàng</div>
                                                <div class="uk-form-controls">
                                                    <div class="uk-grid-small" uk-grid>
                                                        <div class="uk-width-auto@s">
                                                            <div class="uk-inline">
                                                                <span class="uk-form-icon uk-form-icon-flip" uk-icon="icon: clock; ratio: <?= $ratio ?>"></span>
                                                                <input class="uk-input uk-border-pill" type="date">
                                                            </div>
                                                        </div>
                                                        <div class="uk-width-auto@s">
                                                            <div class="uk-inline">
                                                                <span class="uk-form-icon uk-form-icon-flip" uk-icon="icon: calendar; ratio: <?= $ratio ?>"></span>
                                                                <input class="uk-input uk-border-pill" type="date">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
            <div class="uk-width-1-3@m">
                <div class="uk-card uk-card-default menu__card3 uk-overflow-hidden" uk-sticky="offset: 100;media: @m;bottom: true;">
                    <div class="uk-card-header">
                        <h3 class="uk-h3 menu__titleThucdon">Món chọn</h3>
                    </div>
                    <table class="uk-table uk-table-divider uk-table-small uk-table-middle uk-margin-remove">
                        <tbody>
                        <?php
                        $data = array(
                            array(
                                'count' => '1',
                                'name' => 'Coffee Capuchino',
                                'price' => '50.000đ',
                            ),
                            array(
                                'count' => '2',
                                'name' => 'Americano',
                                'price' => '50.000đ',
                            ),
                        );
                        foreach ($data as $k => $v): ?>
                        <tr>
                            <td>
                                <div class="uk-grid-5 uk-flex-middle" uk-grid>
                                    <div>
                                        <a href="#" class="uk-icon-link" uk-icon="icon:plus-circle;ratio:0.7"></a>
                                    </div>
                                    <div>
                                        <div class="uk-position-relative menu__card3__table__w1">
                                            <span class="uk-position-center menu__card3__table__txt2"><?= $v['count'] ?></span>
                                        </div>
                                    </div>
                                    <div>
                                        <a href="#" class="uk-icon-link" uk-icon="icon:minus-circle;ratio:0.7"></a>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <a href="#" class="uk-icon-link" uk-icon="icon:trash;ratio:0.7"></a>
                                <span class="uk-text-middle menu__card3__table__txt1"><?= $v['name'] ?></span>
                            </td>
                            <td>
                                <span class="menu__card3__table__txt1"><?= $v['price'] ?></span>
                            </td>
                        </tr>
                        <?php endforeach; ?>
                        <tr>
                            <td colspan="3">
                                <div class="uk-margin-small">
                                    <div class="uk-position-relative">
                                        <input class="uk-input menu__card3__box1__input uk-border-rounded" type="text" placeholder="Mã khuyến mãi của bạn">
                                        <button type="button" class="uk-button menu__card3__box1__btn uk-background-secondary uk-button-small uk-position-center-right uk-position-small uk-border-rounded">ÁP DỤNG</button>
                                    </div>
                                </div>
                                <div class="uk-margin-small">
                                    <div class="uk-alert-success uk-margin-remove" uk-alert>
                                        <a class="uk-alert-close menu__card3__box1__close" uk-close></a>
                                        <p class="menu__card3__box1__txt">Bạn đã được giảm giá 10.000đ</p>
                                    </div>
                                </div>
                                <a href="#modal-confirm" class="uk-link-toggle" uk-toggle>
                                    <div class="menu__card3__table__btn">
                                        <div class="uk-flex-middle" uk-grid>
                                            <div class="uk-width-expand">
                                                <div class="menu__card3__submit__txt1">3 món</div>
                                                <div class="menu__card3__submit__txt2"><del>150.000đ</del></div>
                                                <div class="menu__card3__submit__txt3">140.000đ</div>
                                            </div>
                                            <div class="uk-width-auto">
                                                <span class="menu__card3__submit__txt4 uk-text-middle uk-margin-small-right">ĐẶT NGAY</span>
                                                <span uk-icon="icon: arrow-right; ratio: 1.2"></span>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </td>
                        </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
<!--Modal xac nhan-->
<div id="modal-confirm" class="uk-flex-top" uk-modal>
    <div class="uk-modal-dialog order__modal__dialog uk-modal-body uk-margin-auto-vertical">

        <button class="uk-modal-close-default" type="button" uk-close></button>

        <div class="uk-text-center">
            <p>Bạn có chắc chắn muốn đặt hàng?</p>
            <a href="#modal-success" uk-toggle class="uk-button uk-button-large uk-button-secondary order__modal__btn">XÁC  NHẬN</a>
        </div>

    </div>
</div>
<!--/Modal xac nhan-->

<!--Modal success-->
<div id="modal-success" class="uk-flex-top" uk-modal>
    <div class="uk-modal-dialog order__modal__dialog uk-modal-body uk-margin-auto-vertical">

        <button class="uk-modal-close-default" type="button" uk-close></button>

        <div class="uk-text-center">
            <div>
                <img src="images/icon/Group997.png" alt="">
            </div>
            <p>Đặt hàng thành công!</p>
        </div>

    </div>
</div>
<!--/Modal success-->

<script>
    // UIkit.modal('#modal-success').show();
</script>
<?php require "template-parts/layouts/footer.php"; ?>