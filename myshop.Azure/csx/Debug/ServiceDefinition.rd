<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="myshop.Azure" generation="1" functional="0" release="0" Id="80f88c1f-5ffd-4446-bc80-83a1cfbbf190" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="myshop.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="myshop:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/myshop.Azure/myshop.AzureGroup/LB:myshop:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="myshop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/myshop.Azure/myshop.AzureGroup/Mapmyshop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="myshopInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/myshop.Azure/myshop.AzureGroup/MapmyshopInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:myshop:Endpoint1">
          <toPorts>
            <inPortMoniker name="/myshop.Azure/myshop.AzureGroup/myshop/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="Mapmyshop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/myshop.Azure/myshop.AzureGroup/myshop/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapmyshopInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/myshop.Azure/myshop.AzureGroup/myshopInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="myshop" generation="1" functional="0" release="0" software="C:\Users\user\Desktop\myshop\myshop_final\myshop.Azure\csx\Debug\roles\myshop" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="81" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;myshop&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;myshop&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/myshop.Azure/myshop.AzureGroup/myshopInstances" />
            <sCSPolicyUpdateDomainMoniker name="/myshop.Azure/myshop.AzureGroup/myshopUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/myshop.Azure/myshop.AzureGroup/myshopFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="myshopUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="myshopFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="myshopInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="e5c48df4-06d4-40e6-bfdf-eb54bcfbb1eb" ref="Microsoft.RedDog.Contract\ServiceContract\myshop.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="8f227140-97c5-4dd3-8d64-116a9a68e33f" ref="Microsoft.RedDog.Contract\Interface\myshop:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/myshop.Azure/myshop.AzureGroup/myshop:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>