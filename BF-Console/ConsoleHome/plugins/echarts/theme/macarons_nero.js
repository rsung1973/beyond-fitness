
(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['exports', 'echarts'], factory);
    } else if (typeof exports === 'object' && typeof exports.nodeName !== 'string') {
        // CommonJS
        factory(exports, require('echarts'));
    } else {
        // Browser globals
        factory({}, root.echarts);
    }
}(this, function (exports, echarts) {
    var log = function (msg) {
        if (typeof console !== 'undefined') {
            console && console.error && console.error(msg);
        }
    };
    if (!echarts) {
        log('ECharts is not Loaded');
        return;
    }
    echarts.registerTheme('macarons_nero', {
        "color": [
            "#ffb980",
            "#d87a80",
            "#2ec7c9",
            "#b6a2de",
            "#5ab1ef",            
            "#8d98b3",
            "#e5cf0d",
            "#97b552",
            "#95706d",
            "#dc69aa",
            "#07a2a4",
            "#9a7fd1",
            "#588dd5",
            "#f5994e",
            "#c05050",
            "#59678c",
            "#c9ab00",
            "#7eb00a",
            "#6f5553",
            "#c14089"
        ],
        "backgroundColor": "#1c222c",
        "textStyle": {},
        "title": {
            "textStyle": {
                "color": "#008acd",
                 "fontWeight": "bold",
            },
            "subtextStyle": {
                "color": "#aaaaaa",
                "fontWeight": "bold",
                "fontSize": "14"
            }
        },
        "line": {
            "itemStyle": {
                "borderWidth": 1
            },
            "lineStyle": {
                "width": 2
            },
            "label" : {
                "show": "true",
                "fontWeight": "bold",
                "position": "top",
                "textBorderColor": "transparent"
            },
            "symbolSize": 10,
            "symbol": "circle",
            "smooth": true
        },
        "radar": {
            "itemStyle": {
                "borderWidth": 1
            },
            "lineStyle": {
                "width": 2
            },
            "symbolSize": 3,
            "symbol": "emptyCircle",
            "smooth": true
        },
        "bar": {      
            "itemStyle": {
                "barBorderWidth": "0",
                "barBorderColor": "#ccc"
            },
            "label" : {
                "show": "true",
                "fontWeight": "bold",
                "position": "top",
                "textBorderColor": "transparent",
                "color": "#fff"
            },
        },
        "pie": {
            "itemStyle": {
                "borderWidth": "0",
                "borderColor": "#ccc"
            }
        },
        "scatter": {
            "itemStyle": {
                "borderWidth": "0",
                "borderColor": "#ccc",
            }
        },
        "boxplot": {
            "itemStyle": {
                "borderWidth": "0",
                "borderColor": "#ccc"
            }
        },
        "parallel": {
            "itemStyle": {
                "borderWidth": "0",
                "borderColor": "#ccc"
            }
        },
        "sankey": {
            "itemStyle": {
                "borderWidth": "0",
                "borderColor": "#ccc"
            }
        },
        "funnel": {
            "itemStyle": {
                "borderWidth": "0",
                "borderColor": "#ccc"
            }
        },
        "gauge": {
            "title": {
                "color": "#fff"
            },
            "axisLabel": {
                "color": "#cccccc"
            },
            "itemStyle": {
                "borderWidth": "0",
                "borderColor": "#ccc",
            }
        },
        "candlestick": {
            "itemStyle": {
                "color": "#d87a80",
                "color0": "#2ec7c9",
                "borderColor": "#d87a80",
                "borderColor0": "#2ec7c9",
                "borderWidth": 1
            }
        },
        "graph": {
            "itemStyle": {
                "borderWidth": "0",
                "borderColor": "#ccc"
            },
            "lineStyle": {
                "width": 1,
                "color": "#aaaaaa"
            },
            "symbolSize": 3,
            "symbol": "emptyCircle",
            "smooth": true,
            "color": [
                "#2ec7c9",
                "#b6a2de",
                "#5ab1ef",
                "#ffb980",
                "#d87a80",
                "#8d98b3",
                "#e5cf0d",
                "#97b552",
                "#95706d",
                "#dc69aa",
                "#07a2a4",
                "#9a7fd1",
                "#588dd5",
                "#f5994e",
                "#c05050",
                "#59678c",
                "#c9ab00",
                "#7eb00a",
                "#6f5553",
                "#c14089"
            ],
            "label": {
                "color": "#eeeeee"
            }
        },
        "map": {
            "itemStyle": {
                "normal": {
                    "areaColor": "#dddddd",
                    "borderColor": "#eeeeee",
                    "borderWidth": 0.5
                },
                "emphasis": {
                    "areaColor": "rgba(254,153,78,1)",
                    "borderColor": "#444",
                    "borderWidth": 1
                }
            },
            "label": {
                "normal": {
                    "textStyle": {
                        "color": "#d87a80"
                    }
                },
                "emphasis": {
                    "textStyle": {
                        "color": "rgb(100,0,0)"
                    }
                }
            }
        },
        "geo": {
            "itemStyle": {
                "normal": {
                    "areaColor": "#dddddd",
                    "borderColor": "#eeeeee",
                    "borderWidth": 0.5
                },
                "emphasis": {
                    "areaColor": "rgba(254,153,78,1)",
                    "borderColor": "#444",
                    "borderWidth": 1
                }
            },
            "label": {
                "normal": {
                    "textStyle": {
                        "color": "#d87a80"
                    }
                },
                "emphasis": {
                    "textStyle": {
                        "color": "rgb(100,0,0)"
                    }
                }
            }
        },               
        "categoryAxis": {                        
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#444d55",
                    //"type": "dashed"
                }
            },
            "axisTick": {
                "show": false,
                "lineStyle": {
                    "color": "#cccccc"
                }
            },
            "axisLabel": {
                "show": true,
                "textStyle": {
                    "color": "#cccccc"
                }
            },
            "splitLine": {
                "show": false,
                "lineStyle": {
                    "color": [
                        "#444d55"
                    ],
                    "type": "dashed"
                }
            },
            "splitArea": {
                "show": false,
                "areaStyle": {
                    "color": [
                        "rgba(0,0,0,0)",
                        "rgba(50, 60, 78, 0.3)"
                    ]
                }
            }
        },
        "valueAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#999999"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#cccccc"
                }
            },
            "axisLabel": {
                "show": true,
                "textStyle": {
                    "color": "#cccccc"
                }
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#444d55"
                    ],
                    "type": "dashed"
                }
            },
            "splitArea": {
                "show": true,
                "areaStyle": {
                    "color": [
                        "transparent"
                    ]
                }
            }
        },
        "logAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#999999"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#cccccc"
                }
            },
            "axisLabel": {
                "show": true,
                "textStyle": {
                    "color": "#cccccc"
                }
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#eee"
                    ]
                }
            },
            "splitArea": {
                "show": true,
                "areaStyle": {
                    "color": [
                        "rgba(250,250,250,0.3)",
                        "rgba(200,200,200,0.3)"
                    ]
                }
            }
        },
        "timeAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#999999"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#cccccc"
                }
            },
            "axisLabel": {
                "show": true,
                "textStyle": {
                    "color": "#eeeeee"
                }
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#eee"
                    ]
                }
            },
            "splitArea": {
                "show": false,
                "areaStyle": {
                    "color": [
                        "rgba(250,250,250,0.3)",
                        "rgba(200,200,200,0.3)"
                    ]
                }
            }
        },
        "toolbox": {
            "iconStyle": {
                "normal": {
                    "borderColor": "#97b552"
                },
                "emphasis": {
                    "borderColor": "#97b552"
                }
            }
        },
        "legend": {
            "textStyle": {
                "color": "#cccccc"
            }
        },
        "tooltip": {
            "axisPointer": {
                "lineStyle": {
                    "color": "#999999",
                    "width": "1"
                },
                "crossStyle": {
                    "color": "#999999",
                    "width": "1"
                }
            }
        },
        "timeline": {
            "lineStyle": {
                "color": "#008acd",
                "width": "3"
            },
            "itemStyle": {
                "normal": {
                    "color": "#008acd",
                    "borderWidth": "1"
                },
                "emphasis": {
                    "color": "#a9334c"
                }
            },
            "controlStyle": {
                "normal": {
                    "color": "#008acd",
                    "borderColor": "#008acd",
                    "borderWidth": 0.5
                },
                "emphasis": {
                    "color": "#008acd",
                    "borderColor": "#008acd",
                    "borderWidth": 0.5
                }
            },
            "checkpointStyle": {
                "color": "#2ec7c9",
                "borderColor": "#2ec7c9"
            },
            "label": {
                "normal": {
                    "textStyle": {
                        "color": "#008acd"
                    }
                },
                "emphasis": {
                    "textStyle": {
                        "color": "#008acd"
                    }
                }
            }
        },
        "visualMap": {
            "textStyle": {
                "color": "#fff"
            },
            "color": [
                "#bf444c",
                "#d88273",
                "#f6efa6"
            ]
        },
        "dataZoom": {
            "backgroundColor": "rgba(47,69,84,0)",
            "dataBackgroundColor": "#efefff",
            "fillerColor": "rgba(182,162,222,0.2)",
            "handleColor": "#008acd",
            "handleSize": "100%",
            "textStyle": {
                "color": "#333333"
            }
        },
        grid: {
            borderColor: '#eee',
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        "markPoint": {
            "label": {
                "color": "#eeeeee"
            },
            "emphasis": {
                "label": {
                    "color": "#eeeeee"
                }
            }
        },
        "calendar": {
            "itemStyle": {
                "normal": {
                    "color": "#1c222c",
                    "borderWidth": 1,
                    "borderColor": "#fff"
                },                
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#444d55"
                    ],
                    "type": "dashed"
                }
            },
            "monthLabel": {
                "show": true,
                "color": "#fff",
                "nameMap": [
                    "一月", "二月", "三月",
                    "四月", "五月", "六月",
                    "七月", "八月", "九月",
                    "十月", "十月", "十二月"
                ]
            },
            "dayLabel": {
                "show": true,
                "color": "#eeeeee",
                "nameMap": "cn",
            },
        },
    });
}));
