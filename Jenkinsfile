pipeline {
    agent any 
    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', credentialsId: '7d45d4df-5f8a-49e0-af51-c0f629e77c4b', url: 'https://github.com/ThuyLam999/BackEnd.git'
            }
        }
        
        stage('Restore packages'){
            steps{
                bat "dotnet restore BackendAPI/BackendAPI.csproj"
            }
        }

        stage('Clean'){
            steps{
                bat "dotnet clean BackendAPI/BackendAPI.csproj"
            }
        }

        stage('Build'){
            steps{
                bat "dotnet build BackendAPI/BackendAPI.csproj --configuration Release"
            }
        }

        stage('Docker Build and Tag') {
            steps {    
                bat 'docker build --tag lptest999/docker_backendapi_test .' 
                bat 'docker tag lptest999/docker_backendapi_test lptest999/docker_backendapi_test'               
            }
        }
     
        stage('Publish image to Docker Hub') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'dockerhubid', passwordVariable: 'password', usernameVariable: 'username')]) {
                    bat 'docker login -u %username% -p %password%'
                    bat 'docker push lptest999/docker_backendapi_test' 
                }  
            }
        }
     
        stage('Run Docker container on Jenkins Agent') {
            steps 
			{
                bat "docker run -d -p 5000:80 lptest999/docker_backendapi_test"
            }
        }

        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
    post {
        success {
            mail bcc: '', body: 'Thông báo kết quả build', cc: '', from: '', replyTo: '', subject: 'Test Run SUCCESSFUL', to: 'thanhthuyyasou234@gmail.com'
        }  

        failure {
            mail bcc: '', body: 'Thông báo kết quả build', cc: '', from: '', replyTo: '', subject: 'Test Run FAILED', to: 'thanhthuyyasou234@gmail.com'
        }
    }
}
