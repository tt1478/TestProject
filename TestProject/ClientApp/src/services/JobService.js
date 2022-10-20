import http from "../http-common";

const getAll = async () => {
    return await http.get("/jobs");
};

const JobService = {
    getAll
};
export default JobService;