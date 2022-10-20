import http from "../http-common";

const getAll = () => {
    return http.get("/jobs");
};

const JobService = {
    getAll
};
export default JobService;