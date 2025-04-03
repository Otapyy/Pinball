using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        
    [SerializeField] private float movePower = 10f; // Aumentei um pouco a força
    [SerializeField] private bool useTorque = false; // Desativei torque para mais controle
    [SerializeField] private float maxAngularVelocity = 10f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float stopFriction = 5f; // Novo: desaceleração manual

    private const float k_GroundRayLength = 1.1f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularVelocity;
        rb.drag = 1.5f; // Novo: Ajuda a desacelerar no ar
        rb.angularDrag = 2f; // Novo: Evita que role demais
    }

    public void Move(Vector3 moveDirection, bool jump)
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 force = moveDirection * movePower;

            if (useTorque)
                rb.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * movePower);
            else
                rb.AddForce(force, ForceMode.Acceleration);
        }
        else
        {
            // Novo: Reduz a velocidade quando o jogador solta os controles
            rb.velocity = new Vector3(rb.velocity.x * (1 - Time.fixedDeltaTime * stopFriction), rb.velocity.y, rb.velocity.z * (1 - Time.fixedDeltaTime * stopFriction));
        }

        if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                }
            }
        }
    }

