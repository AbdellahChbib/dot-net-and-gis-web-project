/* require(["esri/Map", "esri/views/MapView", "esri/layers/GeoJSONLayer", "esri/widgets/Expand"], (
  Map,
  MapView,
  GeoJSONLayer,
  Expand
) => {
  const url = "/api/vols/geojson";

  const layer = new GeoJSONLayer({
    url: url,
    title: "Vols disponibles",
    popupTemplate: {
      title: "{nomVol}",
      content: `
        <b>Départ:</b> {depart}<br/>
        <b>Destination:</b> {destination}<br/>
        <b>Places max:</b> {nbPlacesMax}<br/>
        <b>Prix:</b> {prix} MAD
      `
    },
    renderer: {
      type: "simple",
      symbol: {
        type: "simple-line",
        color: "#0077ff",
        width: 3
      }
    }
  });

  const map = new Map({
    basemap: "streets-navigation-vector",
    layers: [layer]
  });

  const view = new MapView({
    container: "mapView",
    map: map,
    center: [-7.6, 33.5], // Maroc
    zoom: 6
  });

  document.getElementById("search-btn").addEventListener("click", () => {
    const depart = document.getElementById("search-depart").value;
    const destination = document.getElementById("search-destination").value;

    const query = new URLSearchParams();
    if (depart) query.append("depart", depart);
    if (destination) query.append("destination", destination);

    layer.url = "/api/vols/geojson?" + query.toString();
    layer.refresh();
  });
});
 */