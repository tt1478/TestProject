import axios from "axios";

export default axios.create({
   baseURL: window.location.origin.toString() + "/api",
  headers: {
    "Content-type": "application/json"
  }
});