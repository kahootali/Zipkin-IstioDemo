# Zipkin-IstioDemo
This is a basic application which shows how to use Istio and Zipkin and see Distributed Tracing logs. This focuses on Minikube but it can be configured for kubernetes cluster as well.

There are two services FrontEnd and Backend, both in .NetCore, The FrontEnd calls the BackEnd. 

First build the Services and make Docker Images out of them. 
```
cd "\Zipkin-IstioDemo\BackEnd" 
dotnet publish .\BackEnd.csproj -c Debug -o .\obj\Docker\
docker build -t backendapp .

cd "\Zipkin-IstioDemo\FrontEnd" 
dotnet publish .\FrontEnd.csproj -c Debug -o .\obj\Docker\
docker build -t frontendapp .
```
Now go to the Yaml Folder and apply the Yaml files...

` cd "\Zipkin-IstioDemo\Yaml"  `

Now first install Istio and Zipkin.

```
kubectl apply -f .\istio.yaml
kubectl apply -f .\zipkin.yaml
```

And then inject the Istio Sidecars with each service, I am manually injecting it, you can inject automatically as well, 
```
istioctl --kubeconfig C:\Users\<UserName>\.kube\config kube-inject -f .\frontend.yaml | kubectl apply -f  -
istioctl --kubeconfig C:\Users\<UserName>\.kube\config kube-inject -f .\backend.yaml | kubectl apply -f  -
```

Now if you check 
```
kubectl get po --all-namespaces

NAMESPACE      NAME                             READY     STATUS    RESTARTS   AGE
default        backendapp-6fd5656757-q9vzc      2/2       Running   0          16m
default        frontendapp-5958dcfd75-254lh     2/2       Running   0          1m
default        statsd-sink-54f5fb9d5c-z9qst     1/1       Running   7          24d
istio-system   istio-ca-57f9bd7ddb-jrdkt        1/1       Running   0          19m
istio-system   istio-egress-6c6b84cd5d-jjbtl    1/1       Running   0          19m
istio-system   istio-ingress-79cf84458b-rlvxl   1/1       Running   0          19m
istio-system   istio-mixer-6fdc6784c7-gwbvt     2/2       Running   0          19m
istio-system   istio-pilot-5f4865659b-njmtm     1/1       Running   0          19m
istio-system   zipkin-77575b5c89-vlflz          1/1       Running   0          15m
kube-system    kube-addon-manager-minikube      1/1       Running   25         58d
kube-system    kube-dns-6fc954457d-km456        3/3       Running   58         58d
kube-system    kubernetes-dashboard-5jhv5       1/1       Running   36         58d
```
You will be seeing 2/2 containers of the backend and frontend app as there is a container running the service and other of the sidecar.
Now, check the services, 
```
kubectl get svc --all-namespaces
NAMESPACE      NAME                   TYPE           CLUSTER-IP   EXTERNAL-IP   PORT(S)                                                  AGE
default        backendapp             NodePort       10.0.0.68    <none>        80:32739/TCP                                             26m
default        frontendapp            NodePort       10.0.0.43    <none>        80:31523/TCP                                             11m
default        kubernetes             ClusterIP      10.0.0.1     <none>        443/TCP                                                  58d
default        statsd-sink            NodePort       10.0.0.201   <none>        8125:32000/UDP,80:32416/TCP                              24d
istio-system   istio-egress           ClusterIP      10.0.0.211   <none>        80/TCP                                                   29m
istio-system   istio-ingress          LoadBalancer   10.0.0.15    <pending>     80:31595/TCP,443:32254/TCP                               29m
istio-system   istio-mixer            ClusterIP      10.0.0.49    <none>        9091/TCP,9093/TCP,9094/TCP,9102/TCP,9125/UDP,42422/TCP   29m
istio-system   istio-pilot            ClusterIP      10.0.0.29    <none>        8080/TCP,443/TCP                                         29m
istio-system   zipkin                 NodePort       10.0.0.7     <none>        9411:31428/TCP                                           25m
kube-system    kube-dns               ClusterIP      10.0.0.10    <none>        53/UDP,53/TCP                                            58d
kube-system    kubernetes-dashboard   NodePort       10.0.0.223   <none>        80:30000/TCP                                             58d
```
You can go to the `<MinikubeIp>:frontendappPort` i.e. IP:32739 in above case and will see 
##### Hello Muhammad Ali Kahoot

and then go to `<minikube ip>:31428` to see zipkin and find traces of the call. 
