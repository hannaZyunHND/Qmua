<script>
    import { Line } from 'vue-chartjs'
    import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips'
    import { mapGetters, mapActions } from "vuex"

    const DateNow = new Date();
    export default {
        extends: Line,
        props: {
            actions: {
                type: Boolean,
                required: false,
                default: false
            }
        },
        data() {
            return {
                monthselected: DateNow.getMonth() + 1,
                yearselected: DateNow.getFullYear(),
                label: [],
                DashBoardData2: []
            }
        },
        methods: {
            ...mapActions(["getDashBoard2"]),
            onLoad() {
                this.getDashBoard2({
                    month: this.monthselected,
                    year: this.yearselected
                }).then(res => {
                    if (res.success) {
                        this.DashBoardData2 = [];
                        this.DashBoardData2 = res.listData;
                        var lstData = this.DashBoardData2;

                        const dataNew = []
                        const dataProcessing = []
                        const dataApproved = []
                        const dataSuccess = []
                        const dataCancle = []


                        for (let i = 0; i < lstData.length; i++) {
                            this.label.push(lstData[i].createdDate);
                            dataNew.push(lstData[i].new);
                            dataProcessing.push(lstData[i].processing);
                            dataApproved.push(lstData[i].approved);
                            dataSuccess.push(lstData[i].success);
                            dataCancle.push(lstData[i].cancle);
                        }
                        this.renderChart({
                            labels: this.label,
                            datasets: [
                                {
                                    label: 'Mới',
                                    backgroundColor: '#63c2de',
                                    fill: false,
                                    borderColor: '#63c2de',
                                    pointHoverBackgroundColor: '#63c2de',
                                    borderWidth: 2,
                                    data: dataNew
                                },
                                {
                                    label: 'Đang xác nhận',
                                    backgroundColor: '#ffc107',
                                    fill: false,
                                    borderColor: '#ffc107',
                                    pointHoverBackgroundColor: '#ffc107',
                                    borderWidth: 2,
                                    data: dataProcessing
                                },
                                {
                                    label: 'Đã xác nhận',
                                    backgroundColor: '#4dbd74',
                                    fill: false,
                                    borderColor: '#4dbd74',
                                    pointHoverBackgroundColor: '#4dbd74',
                                    borderWidth: 1,
                                    data: dataApproved
                                },
                                {
                                    label: 'Thành công',
                                    backgroundColor: '#6f42c1',
                                    fill: false,
                                    borderColor: '#6f42c1',
                                    pointHoverBackgroundColor: '#6f42c1',
                                    borderWidth: 2,
                                    data: dataSuccess
                                },
                                {
                                    label: 'Hủy',
                                    backgroundColor: '#f86c6b',
                                    fill: false,
                                    borderColor: '#f86c6b',
                                    pointHoverBackgroundColor: '#f86c6b',
                                    borderWidth: 2,
                                    data: dataCancle
                                }
                            ]
                        }, {
                                tooltips: {
                                    enabled: false,
                                    custom: CustomTooltips,
                                    intersect: true,
                                    mode: 'index',
                                    position: 'nearest',
                                    callbacks: {
                                        labelColor: function (tooltipItem, chart) {
                                            return { backgroundColor: chart.data.datasets[tooltipItem.datasetIndex].borderColor }
                                        }
                                    }
                                },
                                maintainAspectRatio: false,
                                legend: {
                                    display: false
                                },
                                scales: {
                                    xAxes: [{
                                        gridLines: {
                                            drawOnChartArea: false
                                        }
                                    }],
                                    yAxes: [{
                                        ticks: {
                                            beginAtZero: true,
                                            maxTicksLimit: 5
                                        },
                                        gridLines: {
                                            display: true
                                        }
                                    }]
                                },
                                elements: {
                                    point: {
                                        radius: 0,
                                        hitRadius: 10,
                                        hoverRadius: 4,
                                        hoverBorderWidth: 3
                                    }
                                }
                            })
                    }
                });
            }
        },
        computed: {
            ...mapGetters(["dashboards"])
        },
        mounted() {
            this.onLoad();
        },
        watch: {
            actions: function (newVal) {
                if (newVal == true) {
                    this.DashBoardData2 = [];
                    this.onLoad();
                }
            }
        }

    }
</script>
