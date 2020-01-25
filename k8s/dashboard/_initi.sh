kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.0.0-beta8/aio/deploy/recommended.yaml
kubectl apply -f serviceaccount.yml
kubectl apply -f clusterRoleBinding.yml