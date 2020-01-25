kubectl apply -f namespace.yml
# kubectl apply -f cronjob.yml -n=etl
kubectl apply -f ingress.yml -n=micro
kubectl apply -f deploy-webapp.yml -n=micro
kubectl apply -f deploy-webstatus.yml -n=micro