
echo Executing: "$0"

concreteServices=$1

echo parameters... concreteServices: $concreteServices

docker-compose -f docker-compose-tests.yml build $concreteServices && docker-compose -f docker-compose-tests.yml up $concreteServices