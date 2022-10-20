import http from "../http-common";

const getAll = async () => {
  return await http.get("/employees");
};

const get = async (id) => {
    return await http.get(`/employees/${id}`);
};

const create = async (data) => {
    return await http.post("/employees", data);
};

const update = async (id, data) => {
    return await http.put(`/employees/${id}`, data);
};

const remove = async (id) => {
    return await http.delete(`/employees/${id}`);
};

const findByName = async (Name) => {
    return await http.get(`/employees?searchTerm=${Name}`);
};

const EmployeeService = {
  getAll,
  get,
  create,
  update,
  remove,
  findByName,
};

export default EmployeeService;
