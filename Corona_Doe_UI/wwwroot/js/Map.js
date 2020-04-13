var currentLocation = { lon: 0, lat: 0 };
var map;
window.MapJsFunctions = {
    showMap: function (lonCoor, latCoor, allowClickOnMap = true, routes = null) {

        var appId = 'p6GqkdoqlSjfnyVvdIUL';
        var appCode = '00CJe6ilEo6UAyApIR0JNA';
        var urlTpl = 'https://{1-4}.{base}.maps.cit.api.here.com' +
            '/{type}/2.1/maptile/newest/{scheme}/{z}/{x}/{y}/256/png' +
            '?app_id={app_id}&app_code={app_code}';

        var osmLayer = new ol.layer.Tile({
            visible: true,
            type: "base",
            preload: Infinity,
            source: new ol.source.OSM()
        });

        var satelliteDesc = {
            base: 'aerial',
            type: 'maptile',
            scheme: 'satellite.day',
            app_id: appId,
            app_code: appCode
        };

        var satelliteLayer = new ol.layer.Tile({
            visible: false,
            type: "base",
            preload: Infinity,
            source: new ol.source.XYZ({
                url: createUrl(urlTpl, satelliteDesc)
            })
        });

        var sourceFeatures = new ol.source.Vector();
        var layerFeatures = new ol.layer.Vector({
            source: sourceFeatures
        });
        var locationFeature;

        var locationStyle = [
            new ol.style.Style({
                image: new ol.style.Icon({
                    scale: 0.7,
                    rotateWithView: false,
                    anchor: [0.5, 0.55],
                    anchorXUnits: 'fraction',
                    anchorYUnits: 'fraction',
                    opacity: 1,
                    src: 'image/marker.png'
                }),
                zIndex: 5
            })
        ];

        //start polyline routes
        var routePaths = [];
        if (routes != null) {
            var routeSplits = routes.split("/");
            var routeStyle = [
                new ol.style.Style({
                    stroke: new ol.style.Stroke({
                        width: 6,
                        color: [100, 149, 237, 0.8]
                    })
                })];

            routeSplits.map(function (l) {
                var lon = l.slice(l.indexOf("[") + 1, l.indexOf(","));
                var lat = l.slice(l.indexOf(",") + 1, l.length);
                var location = [parseFloat(lon), parseFloat(lat)];
                routePaths.push(location);
            });
            var route = new ol.geom.LineString(routePaths).transform('EPSG:4326', 'EPSG:3857');
            var routeCoords = route.getCoordinates();

            var routeFeature = new ol.Feature({
                type: 'route',
                geometry: route
            });
            var geoMarker = new ol.Feature({
                type: 'geoMarker',
                geometry: new ol.geom.Point(routeCoords[0])
            });

            routeFeature.setStyle(routeStyle);

            sourceFeatures.addFeature(routeFeature);
            sourceFeatures.addFeature(geoMarker);
        }


        //end polyline routes

        var center = new ol.proj.fromLonLat([currentLocation.lon, currentLocation.lat]);

        if ((lonCoor !== null && latCoor !== null) && (lonCoor !== 0 && latCoor !== 0)) {
            //add marker on map
            locationFeature = new ol.Feature({
                geometry: new ol.geom.Point(new ol.proj.fromLonLat([lonCoor, latCoor]))
            });
            locationFeature.setStyle(locationStyle);
            sourceFeatures.addFeature(locationFeature);

            center = new ol.proj.fromLonLat([lonCoor, latCoor]);
        }

        createMap();

        if (routePaths != null && routePaths.length > 0) {
            var rpaths = routePaths[0]; 
            map.getView().setCenter(new ol.proj.fromLonLat([rpaths[0], rpaths[1]]));
        }

        var geolocation = new ol.Geolocation();
        geolocation.setTracking(true); // here the browser may ask for confirmation

        geolocation.on('change', function () {
            var coordinates = geolocation.getPosition();
            if ((lonCoor === null && latCoor === null) || (lonCoor === 0 && latCoor === 0)) {
                if (routePaths != null && routePaths.length > 0) {
                    var rpaths = routePaths[0]; 
                    map.getView().setCenter(new ol.proj.fromLonLat([rpaths[0], rpaths[1]]));
                } else {
                    currentLocation.lon = coordinates[0];
                    currentLocation.lat = coordinates[1];

                    map.getView().setCenter(new ol.proj.fromLonLat([currentLocation.lon, currentLocation.lat]));
                }
                
            }
        });

        function createMap() {
            var elem = document.getElementById("map");
            if (elem) {
                elem.innerHTML = "";
            }  

            map = new ol.Map({
                target: 'map',
                layers: [osmLayer, satelliteLayer, layerFeatures],
                controls: ol.control.defaults().extend([
                    new ol.control.FullScreen()
                ]),
                view: new ol.View({
                    center: center,
                    zoom: 13
                })
            });

            map.on('singleclick', function (evt) {
                if (!allowClickOnMap) return;
                var coordinate = evt.coordinate;
                var lonlat = new ol.proj.toLonLat(coordinate);
                var feature = new ol.Feature({
                    type: 'click',
                    geometry: new ol.geom.Point(new ol.proj.fromLonLat([lonlat[0], lonlat[1]]))
                });
                feature.setStyle(locationStyle);
                sourceFeatures.clear();
                sourceFeatures.addFeature(feature);
                var lonlatobj = { lon: lonlat[0], lat: lonlat[1] };
                DotNet.invokeMethodAsync('Corona_Doe_UI', 'GetLonLat', lonlatobj);
            });
        }

        function createUrl(tpl, layerDesc) {
            return tpl
                .replace('{base}', layerDesc.base)
                .replace('{type}', layerDesc.type)
                .replace('{scheme}', layerDesc.scheme)
                .replace('{app_id}', layerDesc.app_id)
                .replace('{app_code}', layerDesc.app_code);
        }        
    }
};


