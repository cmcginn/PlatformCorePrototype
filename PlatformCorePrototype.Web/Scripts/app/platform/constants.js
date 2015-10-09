/**=========================================================
 * Module: constants.js
 * Define constants to inject across the application
 =========================================================*/

myApp
    .constant('CUSTOM_APP_PROPERTIES', {
        basePath: appRoot,
        apiBasePath: appRoot + 'api',
        templateBasePath: appRoot + 'Scripts/app/platform/directives/templates',
        myColors: ['#99d8c9', '#66c2a4', '#41ae76', '#238b45', '#006d2c', '#00441b', '#bfd3e6', '#9ebcda', '#8c96c6', '#8c6bb1', '#88419d', '#810f7c', '#4d004b', '#a8ddb5', '#7bccc4', '#4eb3d3', '#2b8cbe', '#0868ac', '#084081', '#fdbb84', '#fc8d59', '#ef6548', '#d7301f', '#b30000', '#7f0000', '#d4b9da', '#c994c7', '#df65b0', '#e7298a', '#ce1256', '#980043', '#67001f', '#fcc5c0', '#fa9fb5', '#f768a1', '#dd3497', '#ae017e', '#7a0177', '#49006a', '#addd8e', '#78c679', '#41ab5d', '#238443', '#006837', '#004529', '#c7e9b4', '#7fcdbb', '#41b6c4', '#1d91c0', '#225ea8', '#253494', '#081d58', '#fee391', '#fec44f', '#fe9929', '#ec7014', '#cc4c02', '#993404', '#662506', '#fed976', '#feb24c', '#fd8d3c', '#fc4e2a', '#e31a1c', '#bd0026', '#800026'],
        pallets: [
            {
                id: 'pallet1', name: 'Pallet 1', colors: [
                '#1f77b4',
                '#aec7e8',
                '#ff7f0e',
                '#ffbb78',
                '#2ca02c',
                '#98df8a',
                '#d62728',
                '#ff9896',
                '#9467bd',
                '#c5b0d5',
                '#8c564b',
                '#c49c94',
                '#e377c2',
                '#f7b6d2',
                '#7f7f7f',
                '#c7c7c7',
                '#bcbd22',
                '#dbdb8d',
                '#17becf',
                '#9edae5'


                ]
            }
            , {
                id: 'pallet2', name: 'Pallet 2', colors: [
                  '#393b79',
                  '#5254a3',
                  '#6b6ecf',
                  '#9c9ede',
                  '#637939',
                  '#8ca252',
                  '#b5cf6b',
                  '#cedb9c',
                  '#8c6d31',
                  '#bd9e39',
                  '#e7ba52',
                  '#e7cb94',
                  '#843c39',
                  '#ad494a',
                  '#d6616b',
                  '#e7969c',
                  '#7b4173',
                  '#a55194',
                  '#ce6dbd',
                  '#de9ed6'

                ]
            }
            , {
                id: 'pallet3', name: 'Pallet 3', colors: ['#3182bd',
            '#6baed6',
            '#9ecae1',
            '#c6dbef',
            '#e6550d',
            '#fd8d3c',
            '#fdae6b',
            '#fdd0a2',
            '#31a354',
            '#74c476',
            '#a1d99b',
            '#c7e9c0',
            '#756bb1',
            '#9e9ac8',
            '#bcbddc',
            '#dadaeb',
            '#636363',
            '#969696',
            '#bdbdbd',
            '#d9d9d9'
                ]
            }

        ]

    })
  .constant('CUSTOM_APP_REQUIRES', {
      // jQuery based and standalone scripts
      scripts: {
          'isteven-multi-select': ['Vendor/angular-multi-select-master/isteven-multi-select.css'],
          'whirl': [appRoot + 'vendor/whirl/dist/whirl.css'],
          'icons': [appRoot + 'Vendor/fontawesome/css/font-awesome.min.css',
                       appRoot + 'Vendor/simple-line-icons/css/simple-line-icons.css']
          //'amcharts': ['vendor/amcharts_3.14.5/amcharts/amcharts.js',
          //'vendor/amcharts_3.14.5/amcharts/serial.js', 'vendor/amcharts_3.14.5/amcharts/xy.js', 'vendor/amcharts_3.14.5/amcharts/themes/light.js']
      },
      // Angular based script (use the right module name)
      modules: [
        { name: 'isteven-multi-select', files: [appRoot + 'Vendor/angular-multi-select-master/isteven-multi-select.js'] },
         {
             name: 'ngDialog', files: [appRoot + 'vendor/ngDialog/js/ngDialog.min.js',
                                                    appRoot + 'vendor/ngDialog/css/ngDialog.min.css',
                                                    appRoot + 'vendor/ngDialog/css/ngDialog-theme-default.min.css']
         }
      ]

  })
;