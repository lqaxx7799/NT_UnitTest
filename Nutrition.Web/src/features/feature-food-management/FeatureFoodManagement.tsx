import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { IFood } from "../../core/models/food.model";
import { Button } from "@fluentui/react-components";

const FeatureFoodManagement = () => {
  const {
    data,
    error,
    isPending: loading,
  } = useQuery({
    queryKey: ['foods'],
    queryFn: () => axios.get<IFood[]>('http://localhost:5259/food/list')
  });

  if (loading) {
    return (
      <div>Loading...</div>
    );
  };

  if (error) {
    return (
      <div>Error...</div>
    );
  }

  return (
    <div>
      <div>Foods</div>
      <div>
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {
              data.data.map((food) => (
                <tr key={food.id}>
                  <td>{food.name}</td>
                  <td>
                    <Button>Edit</Button>
                  </td>
                </tr>
              ))
            }
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default FeatureFoodManagement;
